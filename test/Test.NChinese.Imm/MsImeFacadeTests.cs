using System;
using System.Threading;
using NChinese.Imm;
using NUnit.Framework;

namespace Test.NChinese.Imm
{
    [TestFixture]
    public class MsImeFacadeTests
    {


        [SetUp]
        public void SetUp()
        {

        }

        private static object[] PinyinTestData =
        {
            new object[]
            {
                "便宜又方便得不得了",
                new string[] { "pián", "yi", "yòu", "fāng", "biàn", "de", "bù", "dé", "liǎo" }
            },
            new object[]
            {
                "皮襖", new string[] { "pí", "ǎo" }
            }
        };

        // 注意：必須使用 Single Threaded Apartment，否則在建立 IFELanguage 的 COM 物件時會失敗（0x80004005)。
        [Apartment(ApartmentState.STA)]
        [TestCaseSource("PinyinTestData")]
        public void Should_GetPinyin_Succeed(string input, string[] expected)
        {
            using (MsImeFacade ime = new MsImeFacade(ImeClass.China))
            {
                // Show conversion mode capabilities.
                Console.WriteLine($"ConversionModeCaps: {ime.ConversionModeCaps}");

                string[] result = ime.GetPinyin(input);

                Assert.AreEqual(result.Length, expected.Length);

                for (int i = 0; i < result.Length; i++)
                {
                    Assert.AreEqual(result[i], expected[i]);
                }
            }
        }

        private static object[] ZhuyinTestData =
        {
            new object[]
            {
                "便宜又方便得不得了",
                new string[] { "ㄆㄧㄢˊ", "ㄧˊ", "ㄧㄡˋ", "ㄈㄤ", "ㄅㄧㄢˋ", "ㄉㄜ", "ㄅㄨˋ", "ㄉㄜˊ", "ㄌㄧㄠˇ" }
            }
        };

        // 注意：必須使用 Single Threaded Apartment，否則在建立 IFELanguage 的 COM 物件時會失敗（0x80004005)。
        [Apartment(ApartmentState.STA)]
        [TestCaseSource("ZhuyinTestData")]
        [Ignore("停用此測試，因為 Windows 10 以後的 IFELanguage 似乎已經無法取得注音字根。")]
        public void Should_GetZhuyin_Succeed(string input, string[] expected)
        {
            using (MsImeFacade ime = new MsImeFacade(ImeClass.China))
            {
                string[] result = ime.GetZhuyin(input);

                Assert.AreEqual(result.Length, expected.Length);

                for (int i = 0; i < result.Length; i++)
                {
                    Assert.AreEqual(result[i], expected[i]);
                }
            }
        }
    }
}
