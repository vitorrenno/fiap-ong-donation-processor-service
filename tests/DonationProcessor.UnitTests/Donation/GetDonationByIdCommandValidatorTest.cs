using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using IdentityCampaign.Application.Features.Donation.GetDonationById;
using IdentityCampaign.IntegrationTests.Commands.Donation;

namespace IdentityCampaign.UnitTests.Donation
{
    public class GetDonationByIdCommandValidatorTest
    {
        private readonly GetDonationByIdValidator _validator = new();

        [Fact]
        public void ValidarComando_Valido_DeveRetornarSucesso()
        {
            var request = GetDonationByIdCommandBuilder.Build();
            var result = _validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Deve_falhar_quando_id_e_guid_vazio()
        {
            var request = GetDonationByIdCommandBuilder.Build() with { Id = Guid.Empty };
            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Id" && e.ErrorMessage == "Id donation is required");
        }

        [Fact]
        public void Deve_conter_exatamente_um_erro_quando_id_vazio()
        {
            var request = GetDonationByIdCommandBuilder.Build() with { Id = Guid.Empty };
            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().HaveCount(1);
            result.Errors.First().PropertyName.Should().Be("Id");
        }

        [Theory]
        [InlineData("550e8400-e29b-41d4-a716-446655440000")]
        [InlineData("6ba7b810-9dad-11d1-80b4-00c04fd430c8")]
        [InlineData("d8e8fca2-dc0d-4a8a-9b1c-2c1a1c1c1c1c")]
        public void Deve_passar_com_diferentes_guids_validos(string guidString)
        {
            var validId = Guid.Parse(guidString);
            var request = GetDonationByIdCommandBuilder.Build() with { Id = validId };
            var result = _validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Deve_passar_quando_id_nao_e_vazio()
        {
            var validId = Guid.NewGuid();
            var request = GetDonationByIdCommandBuilder.Build() with { Id = validId };
            var result = _validator.Validate(request);

            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Deve_falhar_apenas_para_guid_empty()
        {
            // Arrange
            var invalidRequest = GetDonationByIdCommandBuilder.Build() with { Id = Guid.Empty };
            var validRequest = GetDonationByIdCommandBuilder.Build() with { Id = Guid.NewGuid() };

            // Act
            var invalidResult = _validator.Validate(invalidRequest);
            var validResult = _validator.Validate(validRequest);

            // Assert
            invalidResult.IsValid.Should().BeFalse();
            validResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Mensagem_de_erro_deve_ser_exata()
        {
            var request = GetDonationByIdCommandBuilder.Build() with { Id = Guid.Empty };
            var result = _validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.First().ErrorMessage.Should().Be("Id donation is required");
        }

        [Fact]
        public void Deve_validar_qualquer_guid_aleatorio()
        {
            // Arrange & Act
            for (int i = 0; i < 100; i++)
            {
                var randomId = Guid.NewGuid();
                var request = GetDonationByIdCommandBuilder.Build() with { Id = randomId };
                var result = _validator.Validate(request);

                // Assert
                result.IsValid.Should().BeTrue($"Falhou na iteração {i} com ID {randomId}");
            }
        }
    }
}
