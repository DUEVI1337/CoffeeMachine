using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeMachine.Application.Dto
{
    /// <summary>
    /// available denomination of banknote
    /// </summary>
    internal enum ValidDenominationBanknote
    {
        Banknote500 = 500,
        Banknote1000 = 1000,
        Banknote2000 = 2000,
        Banknote5000 = 5000
    }

    /// <summary>
    /// Banknote
    /// </summary>
    public class BanknoteDto : IComparable<BanknoteDto>
    {
        /// <summary>
        /// count of banknote deposited in coffee machine
        /// </summary>
        public int CountBanknote { get; set; }

        /// <summary>
        /// denomination banknote (100руб, 200руб).
        /// Valid values: 500, 1000, 2000, 5000
        /// </summary>
        [EnumDataType(typeof(ValidDenominationBanknote), ErrorMessage = "Invalid denomination banknote")]
        public int Denomination { get; set; }

        /// <summary>
        /// Specify property by which will sort List
        /// </summary>
        /// <param name="banknote"></param>
        /// <returns></returns>
        public int CompareTo(BanknoteDto banknote)
        {
            return Denomination.CompareTo(banknote.Denomination);
        }
    }
}