using System.Collections.Generic;
using System.Linq;
using Aiirh.Basic.Collections;
using Aiirh.Basic.Utilities;
using NUnit.Framework;

namespace Aiirh.CommonLibraries.Tests
{
	[TestFixture]
	public class NumericRangeTests
    {

		[Test]
		[TestCaseSource(nameof(GetTestDataForGroups))]
		public void ReGroup(IEnumerable<NumericRange> rangesInput, IEnumerable<NumericRange> rangesOutput, string testId)
		{
			var actual = rangesInput.ReGroupRanges();
			var areIdentical = actual.CompareCollections(rangesOutput);
			Assert.AreEqual(true, areIdentical);
		}

		[Test]
		[TestCaseSource(nameof(GetTestDataForMerge))]
		public void Merge_ShouldReturnResult(NumericRange one, NumericRange another, NumericRange expected, string testId)
		{
			var actual = one.Merge(another);
			Assert.AreEqual(expected, actual);
		}

		[Test]
		[TestCaseSource(nameof(GetTestDataForTakeValues))]
		public void TakeValues(IEnumerable<NumericRange> rangesInput, int count, IEnumerable<int> values, string testId)
		{
			var rangesInputList = rangesInput.ToList();
			var actualValues = rangesInputList.TakeValues(count);
			Assert.AreEqual(values, actualValues);
		}

		[Test]
		[TestCaseSource(nameof(GetTestDataForConcatNumbers))]
		public void Concat_Numbers(IEnumerable<NumericRange> rangesInput, IEnumerable<int> values, IEnumerable<NumericRange> rangesExpected, string testId)
		{
			var actualValues = rangesInput.Concat(values);
			Assert.AreEqual(rangesExpected, actualValues);
		}

		[Test]
		[TestCaseSource(nameof(GetTestDataForConcatRanges))]
		public void Concat_NumericRange(IEnumerable<NumericRange> rangesInput, NumericRange range, IEnumerable<NumericRange> rangesExpected, string testId)
		{
			var actualValues = rangesInput.Concat(range);
			Assert.AreEqual(rangesExpected, actualValues);
		}

		[Test]
		[TestCaseSource(nameof(GetTestDataExcludeOne))]
		public void Exclude_One(NumericRange initialRange, NumericRange rangeToExclude, IEnumerable<NumericRange> rangesExpected, string testId)
		{
			var actualValues = initialRange.Exclude(rangeToExclude);
			Assert.AreEqual(rangesExpected, actualValues);
		}

		[Test]
		[TestCaseSource(nameof(GetTestDataExcludeMany))]
		public void Exclude_Many(NumericRange initialRange, IEnumerable<NumericRange> rangesToExclude, IEnumerable<NumericRange> rangesExpected, string testId)
		{
			var actualValues = initialRange.Exclude(rangesToExclude);
			Assert.AreEqual(rangesExpected, actualValues);
		}

		[Test]
		[TestCaseSource(nameof(GetTestIntersects))]
		public void Intersects(NumericRange initialRange, NumericRange anotherRange, bool expected, string testId)
		{
			var actual = initialRange.Intersects(anotherRange, out _, out _);
			Assert.AreEqual(expected, actual);
		}

		[Test]
		[TestCaseSource(nameof(GetTestIntersectsOrCommonBorder))]
		public void IntersectsOrCommonBorder(NumericRange initialRange, NumericRange anotherRange, bool expected, string testId)
		{
			var actual = initialRange.IntersectsOrCommonBorder(anotherRange, out _, out _);
			Assert.AreEqual(expected, actual);
		}

		private static IEnumerable<TestCaseData> GetTestDataForGroups()
		{
			var input1 = new[] { new NumericRange(10, 15), new NumericRange(1, 4), new NumericRange(1, 6), new NumericRange(9, 10) };
			var output1 = new[] { new NumericRange(1, 6), new NumericRange(9, 15) };
			yield return new TestCaseData(input1, output1, "D6D7898E-F326-4D25-B0EC-9D742C3E2323");

			var input2 = new[] { new NumericRange(1, 20), new NumericRange(1, 4), new NumericRange(1, 6), new NumericRange(20, 20) };
			var output2 = new[] { new NumericRange(1, 20) };
			yield return new TestCaseData(input2, output2, "5411547F-6E18-4B60-AD7A-4A812DEE5A9E");
		}

		private static IEnumerable<TestCaseData> GetTestDataForMerge()
		{
			// Similar or one-element ranges
			yield return new TestCaseData(new NumericRange(1, 20), new NumericRange(1, 20), new NumericRange(1, 20), "9D436509-5978-4A56-9D89-0449675EDBAB");
			yield return new TestCaseData(new NumericRange(1, 1), new NumericRange(1, 1), new NumericRange(1, 1), "23565A33-23DF-44E1-A022-2ECAE333CDB2");
			yield return new TestCaseData(new NumericRange(1, 1), new NumericRange(2, 2), new NumericRange(1, 2), "CA7B5229-DB51-40AE-9D83-AA451B0D8A2B");

			// Adding one-element range
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(11, 11), new NumericRange(1, 11), "10EE3060-1E70-40C8-863A-6E7844EBFAF5");
			yield return new TestCaseData(new NumericRange(1, 1), new NumericRange(2, 20), new NumericRange(1, 20), "A8AECBA4-179D-44AB-B05A-19117D8534ED");

			// Normal ranges
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(11, 20), new NumericRange(1, 20), "DA068095-9074-413E-8985-DA401F98B528");
			yield return new TestCaseData(new NumericRange(10, 20), new NumericRange(1, 11), new NumericRange(1, 20), "32C57A3A-432E-4BD7-8A8B-1E4222B702EF");
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(11, 20), new NumericRange(1, 20), "690345AF-ED67-473A-BE6C-F31712B0D184");
			yield return new TestCaseData(new NumericRange(1, 11), new NumericRange(10, 20), new NumericRange(1, 20), "6E326187-756C-4895-8BAC-09EF42C477E5");

			// Add embedded range
			yield return new TestCaseData(new NumericRange(1, 20), new NumericRange(5, 15), new NumericRange(1, 20), "6C772310-2219-41AF-8EA0-2CA9D88220B8");
			yield return new TestCaseData(new NumericRange(1, 20), new NumericRange(1, 10), new NumericRange(1, 20), "F1AF696D-BC28-4B3D-BB62-336EB828DFFC");
			yield return new TestCaseData(new NumericRange(1, 20), new NumericRange(10, 20), new NumericRange(1, 20), "0A6D94AB-998D-4950-829B-B46D7D5873CC");
		}

		private static IEnumerable<TestCaseData> GetTestDataForTakeValues()
		{
			var initial = new[] { new NumericRange(1, 4), new NumericRange(7, 7), new NumericRange(10, 20) };
			const int count = 7;
			var expectedNumbers = new[] { 1, 2, 3, 4, 7, 10, 11 };
			yield return new TestCaseData(initial, count, expectedNumbers, "2A93C4BC-4747-47E1-B572-F4846FCF25C3");
		}

		private static IEnumerable<TestCaseData> GetTestDataForConcatNumbers()
		{
			var initial1 = new[] { new NumericRange(1, 1) };
			var values1 = new[] { 1, 2, 3, 4, 7, 10, 11 };
			var expected1 = new[] { new NumericRange(1, 4), new NumericRange(7, 7), new NumericRange(10, 11) };
			yield return new TestCaseData(initial1, values1, expected1, "398BC231-40F4-4983-8989-0D38B54AE0A6");

			var initial2 = Enumerable.Empty<NumericRange>();
			var values2 = new[] { 1, 2, 3, 4, 7, 10, 11 };
			var expected2 = new[] { new NumericRange(1, 4), new NumericRange(7, 7), new NumericRange(10, 11) };
			yield return new TestCaseData(initial2, values2, expected2, "2586A745-FE81-40F4-A8F4-DBBC45C185F1");
		}

		private static IEnumerable<TestCaseData> GetTestDataForConcatRanges()
		{
			var initial1 = new[] { new NumericRange(1, 10), new NumericRange(20, 30) };
			var range1 = new NumericRange(5, 45);
			var expected1 = new[] { new NumericRange(1, 45) };
			yield return new TestCaseData(initial1, range1, expected1, "F821A71C-0F3D-4618-94EF-4D2BBB51ACAE");
			var initial2 = new[] { new NumericRange(1, 10), new NumericRange(20, 30) };
			var range2 = new NumericRange(40, 50);
			var expected2 = new[] { new NumericRange(1, 10), new NumericRange(20, 30), new NumericRange(40, 50) };
			yield return new TestCaseData(initial2, range2, expected2, "46899D4F-8564-4C41-8B0F-657EB45EFB27");
			var initial3 = new[] { new NumericRange(1, 10), new NumericRange(20, 30) };
			var range3 = new NumericRange(5, 25);
			var expected3 = new[] { new NumericRange(1, 30) };
			yield return new TestCaseData(initial3, range3, expected3, "B8592798-BEE6-4BBA-A9E6-ADE81F069C0F");
		}

		private static IEnumerable<TestCaseData> GetTestDataExcludeOne()
		{
			var initial = new NumericRange(1, 100);
			var exclude1 = new NumericRange(5, 10);
			var expected1 = new[] { new NumericRange(1, 4), new NumericRange(11, 100) };
			yield return new TestCaseData(initial, exclude1, expected1, "8DDA4B4B-EB44-4574-A642-AA002B2815E6");

			var exclude2 = new NumericRange(-10, 10);
			var expected2 = new[] { new NumericRange(11, 100) };
			yield return new TestCaseData(initial, exclude2, expected2, "6191F0A6-9F84-4A25-B7C0-8D55738CFAD4");

			var exclude3 = new NumericRange(50, 200);
			var expected3 = new[] { new NumericRange(1, 49) };
			yield return new TestCaseData(initial, exclude3, expected3, "1C5C388A-23B1-4C46-9199-E7B3B62BDACA");

			var exclude4 = new NumericRange(-10, 110);
			var expected4 = Enumerable.Empty<NumericRange>();
			yield return new TestCaseData(initial, exclude4, expected4, "AC24151B-7633-499C-8239-4891C3137B06");

			var exclude5 = new NumericRange(1, 100);
			var expected5 = Enumerable.Empty<NumericRange>();
			yield return new TestCaseData(initial, exclude5, expected5, "791AF0D3-672D-44F2-9ADE-E1F00ED14AF6");

			var exclude6 = new NumericRange(101, 200);
			var expected6 = new[] { new NumericRange(1, 100) };
			yield return new TestCaseData(initial, exclude6, expected6, "681A3444-2D07-465F-AA67-5CDB5CDBF786");
		}

		private static IEnumerable<TestCaseData> GetTestDataExcludeMany()
		{

			var initial = new NumericRange(1, 100);
			var toExclude1 = new[] { new NumericRange(1, 5), new NumericRange(10, 20), new NumericRange(15, 30), new NumericRange(60, 70), new NumericRange(90, 110) };
			var expected1 = new[] { new NumericRange(6, 9), new NumericRange(31, 59), new NumericRange(71, 89) };
			yield return new TestCaseData(initial, toExclude1, expected1, "85F1B3B5-E8E2-4FD3-93E8-F9E4700BD7D3");

			var toExclude2 = new[] { new NumericRange(1, 100) };
			var expected2 = Enumerable.Empty<NumericRange>();
			yield return new TestCaseData(initial, toExclude2, expected2, "1C0E09CA-B3EF-4C5A-9E5B-5727D600981C");

			var toExclude3 = new[] { new NumericRange(1, 50), new NumericRange(51, 70), new NumericRange(71, 100) };
			var expected3 = Enumerable.Empty<NumericRange>();
			yield return new TestCaseData(initial, toExclude3, expected3, "06D14A19-1B26-4BE1-A050-06F101B71281");

			var toExclude4 = new[] { new NumericRange(1, 50), new NumericRange(51, 70), new NumericRange(71, 100) };
			var expected4 = Enumerable.Empty<NumericRange>();
			yield return new TestCaseData(initial, toExclude4, expected4, "5E083072-9FB2-4250-A15D-FD5A3DC0A434");

			var toExclude5 = Enumerable.Empty<NumericRange>();
			var expected5 = new[] { new NumericRange(1, 100) };
			yield return new TestCaseData(initial, toExclude5, expected5, "D33C49B0-ADC6-445F-AA95-729B7DBE0031");

			var toExclude6 = new[] { new NumericRange(1, 1), new NumericRange(100, 100) };
			var expected6 = new[] { new NumericRange(2, 99) };
			yield return new TestCaseData(initial, toExclude6, expected6, "F6F81B23-9ACD-473F-93C3-0881C33D0EC7");

			var toExclude7 = new[] { new NumericRange(10, 15), new NumericRange(30, 40) };
			var expected7 = new[] { new NumericRange(1, 9), new NumericRange(16, 29), new NumericRange(41, 100) };
			yield return new TestCaseData(initial, toExclude7, expected7, "F113ABCC-00D4-496D-B7AE-441E845269D3");

			var toExclude8 = new[] { new NumericRange(150, 200), new NumericRange(300, 400) };
			var expected8 = new[] { new NumericRange(1, 100) };
			yield return new TestCaseData(initial, toExclude8, expected8, "85E2EC53-A19A-4AED-9FA3-5C3F5CBC52A4");

			var toExclude9 = new[] { new NumericRange(-100, -50), new NumericRange(300, 400) };
			var expected9 = new[] { new NumericRange(1, 100) };
			yield return new TestCaseData(initial, toExclude9, expected9, "ADAC9EC2-1186-4FE4-9FAA-FCE2CA3966C8");
		}

		private static IEnumerable<TestCaseData> GetTestIntersects()
		{
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(1, 10), true, "7DF83771-1F8A-4098-9B0D-B53C50BE27EC");
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(1, 11), true, "DFAB2A5B-EF6C-41AB-ACC7-DC17F8064FCC");
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(-10, 10), true, "7ECF4558-2028-43DD-B136-7654B7F3B027");
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(-10, 9), true, "48EF5848-7B93-47E6-921E-AF031963861B");
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(2, 9), true, "929246B2-6046-4950-BC3D-6F8214F95508");
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(2, 11), true, "0AD0F798-A3C2-49CA-B3BE-A4106B30DE33");
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(0, 9), true, "6FEE1F33-E64C-4EC0-8C9D-1C8A4CE8C6D8");
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(0, 11), true, "435B77C9-E8F6-4F49-B833-F60834586965");
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(-10, 0), false, "E161D4AF-220D-4320-B841-3E90E3D129AD");
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(11, 20), false, "8F8D0D07-5C7E-4628-9173-6E7CA56F543F");
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(12, 20), false, "074B88E4-12E0-4AE1-A2CB-F5FD57E4B19C");
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(-10, -1), false, "D8B04256-4C38-48F9-8684-09AF1FF7D0DE");
		}

		private static IEnumerable<TestCaseData> GetTestIntersectsOrCommonBorder()
		{
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(1, 10), true, "69756FF0-04A4-4892-AF7C-73515061CE05");
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(1, 11), true, "35FEE3D4-E382-4C92-AD55-6FC4697C003F");
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(-10, 10), true, "F0C01065-243A-41A3-9F70-17D936CF7065");
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(-10, 9), true, "60B2C843-3DDA-4F32-B251-0E30FE4CCC82");
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(2, 9), true, "5FA2FF7F-67B5-4752-9DE6-9168742382D8");
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(2, 11), true, "F023B472-7FF7-43AA-AC2B-0871DC183B0A");
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(0, 9), true, "6D25D938-9E5F-479B-9C2B-93811F45E2D4");
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(0, 11), true, "0ACD3680-6B2A-43F4-9447-4C8CD10A7297");
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(-10, 0), true, "66F6BD3A-1C90-4C29-A3D7-7FCAF24BCE29");
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(11, 20), true, "8DC4A605-28C5-42B8-9B06-D258E2EC58FF");
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(12, 20), false, "2C8C85FC-1779-4FFE-B624-C3B5BFA9C8A4");
			yield return new TestCaseData(new NumericRange(1, 10), new NumericRange(-10, -1), false, "5BABC9D4-5FE0-4CBA-BE11-2B3802F11829");
		}
	}
}