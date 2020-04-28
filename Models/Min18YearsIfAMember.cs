using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Min18YearsIfAMember : ValidationAttribute // dodajemy dziedzieczenie aby klasa ta mogła stać się data adnotations
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //wpierw sprawdzamy wybrany typ członkostwa

            //ustawiamy dostęp do klasy  zawierającej typ członkostwa
            var customer = (Customer) validationContext.ObjectInstance;

            //sprawdzamy wybrany typ członkostwa
            if (customer.MembershipTypeId == MembershipType.Unknow || 
                customer.MembershipTypeId == MembershipType.PayAsYouGo)
            // 1 oznacza -PayAsYouGo którego nie dotyczy validacja daty urodzenia / to samo możemy zoribc dla 2 - Monthly itd.
            // po zmianie 1 na MembershipType.Unknow 2 na MembershipType.PayAsYouGo
            {
                return ValidationResult.Success;
            }

            //w przeciwnym wypadku sprawdzamy najpierw czy Birthday jest null i jak tak to wywala errorMessage
            if (customer.Birthdate == null)
            {
                return new ValidationResult("Birthday is required.");
            }

            //obliczmay jaki jest wiek podany na podstawie daty urodzenia i dnia dzisiejszego
            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;
            
            //zwracamy jeżeli age jest większe od 18 to Success a jezeli ine to komunikat
            return (age >= 18
                ? ValidationResult.Success
                : new ValidationResult("Customer should be at least 18 years old to go a membership."));
        }
    }
}
/* ValidationResult.Success
 * Represents the success of the validation (true if validation was successful; otherwise, false).
*/
