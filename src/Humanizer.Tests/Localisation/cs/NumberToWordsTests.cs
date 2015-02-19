using Xunit;
using Xunit.Extensions;

namespace Humanizer.Tests.Localisation.csCZ
{
    public class NumberToWordsTests : AmbientCulture
    {
        public NumberToWordsTests() : base("cs-CZ") { }

        [Theory]
        [InlineData(0, "nula")]
        [InlineData(1, "jeden")] // TODO:Šeby:Rod: Zatím nemáme implementovaný číslovkový rod, ale správně by tady mělo být „jedna“.
        [InlineData(2, "dva")]
        [InlineData(3, "tři")]
        [InlineData(4, "čtyři")]
        [InlineData(5, "pět")]
        [InlineData(6, "šest")]
        [InlineData(7, "sedm")]
        [InlineData(8, "osm")]
        [InlineData(9, "devět")]
        [InlineData(10, "deset")]
        [InlineData(11, "jedenáct")]
        [InlineData(12, "dvanáct")]
        [InlineData(13, "třináct")]
        [InlineData(14, "čtrnáct")]
        [InlineData(15, "patnáct")]
        [InlineData(16, "šestnáct")]
        [InlineData(17, "sedmnáct")]
        [InlineData(18, "osmnáct")]
        [InlineData(19, "devatenáct")]
        [InlineData(20, "dvacet")]
        [InlineData(30, "třicet")]
        [InlineData(40, "čtyřicet")]
        [InlineData(50, "padesát")]
        [InlineData(60, "šedesát")]
        [InlineData(70, "sedmdesát")]
        [InlineData(80, "osmdesát")]
        [InlineData(90, "devadesát")]
        [InlineData(100, "jedno sto")]
        [InlineData(112, "jedno sto dvanáct")]
        [InlineData(128, "jedno sto dvacet osm")]
        [InlineData(1000, "jeden tisíc")]
        [InlineData(2000, "dva tisíce")]
        [InlineData(5000, "pět tisíc")]
        [InlineData(10000, "deset tisíc")]
        [InlineData(20000, "dvacet tisíc")]
        [InlineData(21000, "dvacet jeden tisíc")]
        [InlineData(22000, "dvacet dva tisíce")]
        [InlineData(25000, "dvacet pět tisíc")]
        [InlineData(100000, "jedno sto tisíc")]
        [InlineData(500000, "pět set tisíc")]
        [InlineData(1000000, "jeden milión")]
        [InlineData(2000000, "dva milióny")]
        [InlineData(5000000, "pět miliónů")]
        [InlineData(1000000000, "jedna miliarda")]
        [InlineData(2000000000, "dvě miliardy")]
        [InlineData(1501001892, "jedna miliarda pět set jeden milión jeden tisíc osm set devadesát dva")]
        [InlineData(2147483647, "dvě miliardy jedno sto čtyřicet sedm miliónů čtyři sta osmdesát tři tisíce šest set čtyřicet sedm")]
        [InlineData(-1501001892, "mínus jedna miliarda pět set jeden milión jeden tisíc osm set devadesát dva")]
        // Following tests are from the site of Institute For Czech Language of The Czech Academy Of Science (http://prirucka.ujc.cas.cz/?id=791&dotaz=%C4%8D%C3%ADsla%20slovy)
        [InlineData(21, "dvacet jeden")] // TODO:Šeby:Rod: Zatím nemáme implementovaný číslovkový rod, ale správně by tady mělo být „jedna“.
        [InlineData(54, "padesát čtyři")]
        [InlineData(900, "devět set")]
        [InlineData(2168, "dva tisíce jedno sto šedesát osm")]
        [InlineData(2231, "dva tisíce dvě stě třicet jeden")] // TODO:Šeby:Rod: Zatím nemáme implementovaný číslovkový rod, ale správně by tady mělo být „jedna“.
        [InlineData(1300768, "jeden milión tři sta tisíc sedm set šedesát osm")] // originally "jeden milion tři sta tisíc sedm set šedesát osm"
        public void ToWords(int number, string expected)
        {
            Assert.Equal(expected, number.ToWords());
        }

        [Theory]
        [InlineData(531, "pět set třicet jedna", GrammaticalGender.Feminine)]
        [InlineData(531, "pět set třicet jeden", GrammaticalGender.Masculine)]
        [InlineData(531, "pět set třicet jedno", GrammaticalGender.Neuter)]
        [InlineData(122, "jedno sto dvacet dvě", GrammaticalGender.Feminine)]
        [InlineData(122, "jedno sto dvacet dva", GrammaticalGender.Masculine)]
        [InlineData(122, "jedno sto dvacet dvě", GrammaticalGender.Neuter)]
        public void ToWordsWithGender(int number, string expected, GrammaticalGender gender)
        {
            Assert.Equal(expected, number.ToWords(gender));
        }
    }
}
