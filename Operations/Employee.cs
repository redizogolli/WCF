using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Operations
{
    [DataContract (Namespace = "")]
    public class Employee : IValidatableObject
    {
        [Required]
        [StringLength(6)]
        [DataMember(Name = "e")]
        public string EMPNO { get; set; }

        [Required]
        [DataMember(Name = "fn")]
        public string FIRSTNME { get; set; }

        [Required]
        [DataMember(Name = "mi")]
        public string MIDINIT { get; set; }

        [Required]
        [DataMember(Name = "ln")]
        public string LASTNAME { get; set; }

        [Required]
        [DataMember(Name = "wd")]
        public string WORKDEPT { get; set; }

        [Required]
        [DataMember(Name = "pn")]
        public string PHONENO { get; set; }

        [DataMember(Name = "hd")]
        public DateTime? HIREDATE { get; set; }

        [Required]
        [DataMember(Name = "j")]
        public string JOB { get; set; }

        [Required]
        [DataMember(Name = "el")]
        public short EDLEVEL { get; set; }

        [Required]
        [DataMember(Name = "s")]
        [StringLength(1)]
        public string SEX { get; set; }

        [DataMember(Name = "bd")]
        public DateTime? BIRTHDATE { get; set; }

        [DataMember(Name = "sal")]
        public decimal? SALARY { get; set; }

        [DataMember(Name = "bo")]
        public decimal? BONUS { get; set; }

        [DataMember(Name = "c")]
        public decimal? COMM { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //first property error
            if(this.EMPNO == null || string.IsNullOrWhiteSpace(this.EMPNO) || this.EMPNO.Length > 6)
            {
                yield return new ValidationResult("No EMPNO information", new string[] { "EMPNO" });
            }

            if (FIRSTNME == null || string.IsNullOrWhiteSpace(this.FIRSTNME))
            {
                yield return new ValidationResult("No FIRSTNME information", new string[] { "FIRSTNME" });
            }

            if (MIDINIT == null || string.IsNullOrWhiteSpace(this.MIDINIT))
            {
                yield return new ValidationResult("No MIDINIT information", new string[] { "MIDINIT" });
            }

            if (LASTNAME == null || string.IsNullOrWhiteSpace(this.LASTNAME))
            {
                yield return new ValidationResult("No LASTNAME information", new string[] { "LASTNAME" });
            }

            if (PHONENO == null || string.IsNullOrWhiteSpace(this.PHONENO))
            {
                yield return new ValidationResult("No PHONENO information", new string[] { "PHONENO" });
            }

            if (this.SEX == null || string.IsNullOrWhiteSpace(this.SEX) || this.SEX.Length > 1)
            {
                yield return new ValidationResult("No SEX information", new string[] { "SEX" });
            }

        }
    }
}