using FluentAssertions;
using IdentityCampaign.Application.Features.Donation.CreateDonation;
using IdentityCampaign.IntegrationTests.Commands.Donation;

namespace IdentityCampaign.UnitTests.Donation
{
    public class CreateDonationCommandValidatorTest
    {
        private readonly CreateDonationValidator _validator = new();

        [Fact]
        public void ValidarComando_Valido_DeveRetornarSucesso()
        {
            // Arrange
            var request = CreateDonationCommandBuilder.Build();

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        #region vAmount Validation Tests

        [Fact]
        public void Deve_falhar_quando_vAmount_e_zero()
        {
            // Arrange
            var request = CreateDonationCommandBuilder.Build() with { vAmount = 0 };

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "vAmount" &&
                e.ErrorMessage == "Donation amount must be greater than zero.");
        }

        [Fact]
        public void Deve_falhar_quando_vAmount_e_negativo()
        {
            // Arrange
            var request = CreateDonationCommandBuilder.Build() with { vAmount = -100 };

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "vAmount" &&
                e.ErrorMessage == "Donation amount must be greater than zero.");
        }

        [Fact]
        public void Deve_falhar_quando_vAmount_e_menor_que_minimo_permitido()
        {
            // Arrange
            var request = CreateDonationCommandBuilder.Build() with { vAmount = 0.5m };

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "vAmount" &&
                e.ErrorMessage == "Minimum donation amount is 1.");
        }

        [Fact]
        public void Deve_passar_quando_vAmount_e_igual_ao_minimo_permitido()
        {
            // Arrange
            var request = CreateDonationCommandBuilder.Build() with { vAmount = 1 };

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Deve_falhar_quando_vAmount_excede_limite_maximo()
        {
            // Arrange
            var request = CreateDonationCommandBuilder.Build() with { vAmount = 1000001 };

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "vAmount" &&
                e.ErrorMessage == "Donation amount exceeds the allowed limit.");
        }

        [Fact]
        public void Deve_passar_quando_vAmount_e_igual_ao_limite_maximo()
        {
            // Arrange
            var request = CreateDonationCommandBuilder.Build() with { vAmount = 1000000 };

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(50.50)]
        [InlineData(500)]
        [InlineData(1000000)]
        public void Deve_passar_com_valores_validos_de_doacao(decimal vAmount)
        {
            // Arrange
            var request = CreateDonationCommandBuilder.Build() with { vAmount = vAmount };

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        #endregion

        #region IdUser Validation Tests

        [Fact]
        public void Deve_falhar_quando_IdUser_e_vazio()
        {
            // Arrange
            var request = CreateDonationCommandBuilder.Build() with { IdUser = Guid.Empty };

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "IdUser" &&
                e.ErrorMessage == "User identifier is required.");
        }

        [Fact]
        public void Deve_passar_quando_IdUser_e_valido()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var request = CreateDonationCommandBuilder.Build() with { IdUser = userId };

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        #endregion

        #region IdCampaign Validation Tests

        [Fact]
        public void Deve_falhar_quando_IdCampaign_e_vazio()
        {
            // Arrange
            var request = CreateDonationCommandBuilder.Build() with { IdCampaign = Guid.Empty };

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.PropertyName == "IdCampaign" &&
                e.ErrorMessage == "Campaign identifier is required.");
        }

        [Fact]
        public void Deve_passar_quando_IdCampaign_e_valido()
        {
            // Arrange
            var campaignId = Guid.NewGuid();
            var request = CreateDonationCommandBuilder.Build() with { IdCampaign = campaignId };

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        #endregion

        #region IdUser vs IdCampaign Validation Tests

        [Fact]
        public void Deve_falhar_quando_IdUser_e_igual_a_IdCampaign()
        {
            // Arrange
            var sameId = Guid.NewGuid();
            var request = CreateDonationCommandBuilder.Build() with
            {
                IdUser = sameId,
                IdCampaign = sameId
            };

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e =>
                e.ErrorMessage == "User identifier cannot be the same as campaign identifier.");
        }

        [Fact]
        public void Deve_passar_quando_IdUser_e_diferente_de_IdCampaign()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var campaignId = Guid.NewGuid();
            var request = CreateDonationCommandBuilder.Build() with
            {
                IdUser = userId,
                IdCampaign = campaignId
            };

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        #endregion

        #region Multiple Validation Errors Tests

        [Fact]
        public void Deve_falhar_com_vAmount_invalido_e_ids_iguais()
        {
            // Arrange
            var sameId = Guid.NewGuid();
            var request = CreateDonationCommandBuilder.Build() with
            {
                vAmount = -50,
                IdUser = sameId,
                IdCampaign = sameId
            };

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "vAmount");
            result.Errors.Should().Contain(e =>
                e.ErrorMessage == "User identifier cannot be the same as campaign identifier.");
        }

        #endregion

        #region Edge Cases Tests

        [Fact]
        public void Deve_passar_com_valor_decimal_pequeno_mas_valido()
        {
            // Arrange
            var request = CreateDonationCommandBuilder.Build() with { vAmount = 1.01m };

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Deve_passar_com_muitas_casas_decimais()
        {
            // Arrange
            var request = CreateDonationCommandBuilder.Build() with { vAmount = 123.4567m };

            // Act
            var result = _validator.Validate(request);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        #endregion
    }
}
