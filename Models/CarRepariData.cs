using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PavlovLabs.Models
{
    public class CarRepairData
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Mark { get; set; }
        public string Model { get; set; }
        //     public string VinCode { public get { return VinCode; } set { VinCode = value.Replace(" ", ""); } }
        public string VinCode { get; set; }
        public string RegNum { get; set; }
        public ushort YearOfIssue { get; set; }

        public BaseModelValidationResult Validate()
        {
            var validationResult = new BaseModelValidationResult();
            if (string.IsNullOrWhiteSpace(Mark)) validationResult.Append($"Mark cannot be empty");
            if (string.IsNullOrWhiteSpace(Model)) validationResult.Append($"Model cannot be empty");
            if (string.IsNullOrWhiteSpace(VinCode) || VinCode.Length != 17) validationResult.Append("Empty or incorrect Vin-code. Vin-code must contain 17 characters");
            // Not add reg. number verification, as the car may not be registered
            if (YearOfIssue < 1940) validationResult.Append("Year of issue cannot be less then 1940");
            else if (YearOfIssue > DateTime.Now.Year) validationResult.Append("Year of issue cannot be greater than the current date");
            return validationResult;
        }

        public override string ToString()
        {
            return $"{Mark} {Model}\n" +
                $"Reg. number: {RegNum}\n" +
                $"Vin code: {VinCode}\n" +
                $"Year of issue: {YearOfIssue}";
        }
    }
}
