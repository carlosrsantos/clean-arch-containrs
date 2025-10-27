using System.ComponentModel.DataAnnotations;

public class MaiorDeIdadeAttribute : ValidationAttribute
{
    public int IdadeMinima { get; }

    public MaiorDeIdadeAttribute(int idadeMinima)
    {
        IdadeMinima = idadeMinima;
        ErrorMessage = $"A pessoa deve ter pelo menos {idadeMinima} anos.";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
            return new ValidationResult("Campo obrigatório.");

        if (value is DateTime dataNascimento)
        {
            var hoje = DateTime.Today;
            var idade = hoje.Year - dataNascimento.Year;

            // Corrige se o aniversário ainda não chegou neste ano
            if (dataNascimento.Date > hoje.AddYears(-idade)) 
                idade--;

            if (idade < IdadeMinima)
                return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}
