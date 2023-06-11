using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Shared
{
    [Table("BaseItems")]
    public class BaseItem
    {
        public int Id { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PersonName { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? Address { get; set; }
        public string? DateOfBirth { get; set; }
        public string? AdditionalInfo { get; set; }
        public State State { get; set; }
        public string? UserLogin { get; set; }
        public int BaseId { get; set; }
        public Base Base { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(PhoneNumber))
            {
                sb.AppendLine(PhoneNumber);
            }
            if (!string.IsNullOrEmpty(PersonName))
            {
                sb.AppendLine(PersonName);
            }
            if (!string.IsNullOrEmpty(City))
            {
                sb.AppendLine(City);
            }
            if (!string.IsNullOrEmpty(Region))
            {
                sb.AppendLine(Region);
            }
            if (!string.IsNullOrEmpty(Address))
            {
                sb.AppendLine(Address);
            }
            if (!string.IsNullOrEmpty(DateOfBirth))
            {
                sb.AppendLine(DateOfBirth);
            }
            if (!string.IsNullOrEmpty(AdditionalInfo))
            {
                sb.AppendLine(AdditionalInfo);
            }
            return sb.ToString();
        }
    }

    public enum State
    {
        New = 0,
        NotAnswered = 1,
        Answered = 2,
        SendedToMeneger = 3
    }
}
