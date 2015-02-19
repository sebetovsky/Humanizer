using System.Collections.Generic;
using System.Globalization;

namespace Humanizer.Localisation.NumberToWords
{
    internal class CzechNumberToWordsConverter : GenderedNumberToWordsConverter
    {
        private static readonly string[] HundredsMap = { "nula", "jedno sto", "dvě stě", "tři sta", "čtyři sta", "pět set", "šest set", "sedm set", "osm set", "devět set" };
        private static readonly string[] TensMap = { "nula", "deset", "dvacet", "třicet", "čtyřicet", "padesát", "šedesát", "sedmdesát", "osmdesát", "devadesát" };
        private static readonly string[] UnitsMap = { "nula", "jedna", "dvě", "tři", "čtyři", "pět", "šest", "sedm", "osm", "devět", "deset", "jedenáct", "dvanáct", "třináct", "čtrnáct", "patnáct", "šestnáct", "sedmnáct", "osmnáct", "devatenáct" };

        private readonly CultureInfo _culture;

        public CzechNumberToWordsConverter(CultureInfo culture)
        {
            _culture = culture;
        }

        private static void CollectPartsUnderThousand(ICollection<string> parts, int number, GrammaticalGender gender)
        {
            var hundreds = number/100;
            if (hundreds > 0)
            {
                parts.Add(HundredsMap[hundreds]);
                number = number%100;
            }

            var tens = number/10;
            if (tens > 1)
            {
                parts.Add(TensMap[tens]);
                number = number%10;
            }

            if (number > 0)
            {
                if (number == 1 && gender == GrammaticalGender.Feminine)
                    parts.Add("jedna");
                else if (number == 1 && gender == GrammaticalGender.Masculine)
                    parts.Add("jeden");
                else if (number == 1 && gender == GrammaticalGender.Neuter)
                    parts.Add("jedno");
                else if (number == 2 && gender == GrammaticalGender.Feminine)
                    parts.Add("dvě");
                else if (number == 2 && gender == GrammaticalGender.Masculine)
                    parts.Add("dva");
                else if (number == 2 && gender == GrammaticalGender.Neuter)
                    parts.Add("dvě");
                else if (number < 20)
                    parts.Add(UnitsMap[number]);
            }
        }

        private static int GetMappingIndex(int number)
        {
            if (number == 1)
                return 0;

            if (number > 1 && number < 5)
                return 1; //denominator

            var tens = number / 10;
            if (tens > 1)
            {
                var unity = number % 10;

                if (unity == 1)
                    return 0;

                if (unity > 1 && unity < 5)
                    return 1; //denominator
            }

            return 2; //genitive
        }

        public override string Convert(int number, GrammaticalGender gender)
        {
            if (number == 0)
                return "nula";

            var parts = new List<string>();

            if (number < 0)
            {
                parts.Add("mínus");
                number = -number;
            }

            // TODO:Šeby:Vyšší čísla: Dopsat vyšší čísla než je miliarda – nutné předělat na decimal

            var milliard = number/1000000000;
            if (milliard > 0)
            {
                // TODO:Šeby:Jeden: pro nezobrazení se nesmí rovnat jedničce.
                if (milliard >= 1)
                    CollectPartsUnderThousand(parts, milliard, GrammaticalGender.Feminine);

                var map = new[] { "miliarda", "miliardy", "miliard" }; //one, denominator, genitive
                parts.Add(map[GetMappingIndex(milliard)]);
                number %= 1000000000;
            }

            var million = number/1000000;
            if (million > 0)
            {
                // TODO:Šeby:Jeden: pro nezobrazení se nesmí rovnat jedničce.
                if (million >= 1)
                    CollectPartsUnderThousand(parts, million, GrammaticalGender.Masculine);

                var map = new[] { "milión", "milióny", "miliónů" }; //one, denominator, genitive
                parts.Add(map[GetMappingIndex(million)]);
                number %= 1000000;
            }

            var thouthand = number/1000;
            if (thouthand > 0)
            {
                // TODO:Šeby:Jeden: pro nezobrazení se nesmí rovnat jedničce.
                if (thouthand >= 1)
                    CollectPartsUnderThousand(parts, thouthand, GrammaticalGender.Masculine);

                var thousand = new[] { "tisíc", "tisíce", "tisíc" }; //one, denominator, genitive
                parts.Add(thousand[GetMappingIndex(thouthand)]);
                number %= 1000;
            }

            var units = number/1;
            if (units > 0)
                CollectPartsUnderThousand(parts, units, gender);

            return string.Join(" ", parts);
        }

        public override string ConvertToOrdinal(int number, GrammaticalGender gender)
        {
            return number.ToString(_culture);
        }
    }
}
