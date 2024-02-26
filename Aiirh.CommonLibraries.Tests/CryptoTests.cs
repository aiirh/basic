using Aiirh.Crypto;
using NUnit.Framework;
using System.Collections.Generic;
using NUnit.Framework.Legacy;

namespace Aiirh.CommonLibraries.Tests;

[TestFixture]
public class CryptoTests
{
    [SetUp]
    public void SetUp()
    {
        CryptographyTools.Init("aa1da334e");
    }

    [Test]
    [TestCaseSource(nameof(GetTestCases))]
    public void EncryptAndDecrypt(string value)
    {
        var encrypted = value.EncryptString();
        var decrypted = encrypted.DecryptString();
        ClassicAssert.AreEqual(value, decrypted);
    }

    private static IEnumerable<TestCaseData> GetTestCases()
    {
        yield return new TestCaseData("eyJQZXJzb25hbENvZGUiOiI2MDAwMTAxOTkwNiIsIkZpcnN0TmFtZSI6Ik1hcnkgw4RubiIsIkxhc3ROYW1lIjoiT+KAmWNvbm5lxb4txaB1c2xpayBUZXN0bnVtYmVyIiwiQmlydGhkYXkiOiIyMDAwLTAxLTAxIiwiQ291bnRyeSI6IkVFIn0=");
    }
}