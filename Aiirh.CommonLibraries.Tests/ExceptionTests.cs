using System;
using System.Collections.Generic;
using Aiirh.Basic.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace Aiirh.CommonLibraries.Tests
{
    [TestFixture]
    public class ExceptionTests
    {

        [Test]
        [TestCaseSource(nameof(GetTestData))]
        public void IsCausedBy(Exception exception, bool expectedResult)
        {
            try
            {
                throw exception;
            }
            catch (Exception ex)
            {
                var actualResult = ex.IsCausedBy<TargetException>();
                actualResult.Should().Be(expectedResult);
            }
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            yield return new TestCaseData(new TargetException(), true);
            yield return new TestCaseData(new DerivedException(), true);
            yield return new TestCaseData(new AnotherException(), false);

            yield return new TestCaseData(new DeepExceptionPositive(), true);
            yield return new TestCaseData(new DeepExceptionNegative(), false);
            yield return new TestCaseData(new VeryDeepExceptionPositive(), true);
            yield return new TestCaseData(new VeryDeepExceptionNegative(), false);

            yield return new TestCaseData(new DeepDerivedExceptionPositive(), true);
            yield return new TestCaseData(new VeryDeepDerivedExceptionPositive(), true);
        }
    }

    internal class TargetException : Exception
    {
    }

    internal class AnotherException : Exception
    {
    }

    internal class DerivedException : TargetException
    {
    }

    internal class DeepExceptionPositive : Exception
    {
        public DeepExceptionPositive() : base("DeepExceptionPositive", new TargetException())
        {
        }
    }

    internal class DeepDerivedExceptionPositive : Exception
    {
        public DeepDerivedExceptionPositive() : base("DeepDerivedExceptionPositive", new DerivedException())
        {
        }
    }

    internal class DeepExceptionNegative : Exception
    {
        public DeepExceptionNegative() : base("DeepExceptionNegative", new AnotherException())
        {
        }
    }

    internal class VeryDeepExceptionPositive : Exception
    {
        public VeryDeepExceptionPositive() : base("VeryDeepExceptionPositive", new DeepExceptionPositive())
        {
        }
    }

    internal class VeryDeepDerivedExceptionPositive : Exception
    {
        public VeryDeepDerivedExceptionPositive() : base("VeryDeepDerivedExceptionPositive", new DeepDerivedExceptionPositive())
        {
        }
    }

    internal class VeryDeepExceptionNegative : Exception
    {
        public VeryDeepExceptionNegative() : base("VeryDeepExceptionNegative", new DeepExceptionNegative())
        {
        }
    }
}