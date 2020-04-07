using NUnit.Framework;
using Parameters;
using System;

namespace Plugin_KOMPAS_3D.UnitTests
{
    public class ParameterTests
    {
        /// <summary>
        /// ���� ������ 
        /// �������� ���������
        /// </summary>
        private Parameter<double> _parameter;

        [SetUp]
        public void CreateParameters()
        {
            _parameter = new Parameter<double>(10, 20, 10, "name");
        }

        [Test(Description = "���������� ���� ������� MaxValue")]
        public void Test_MaxValue_Set_CorrectValue()
        {
            var expected = 20;
            _parameter.MaxValue = expected;
            Assert.AreEqual(expected, _parameter.MaxValue, 
                "������ MaxValue ����������� ���������� ��������");
        }

        [Test(Description = "���������� ���� ������� MaxValue")]
        public void Test_MaxValue_Set_CorrectValue_LessMinValue()
        {
            var expected = 10;
            _parameter.MaxValue = 5;
            Assert.AreEqual(expected, _parameter.MaxValue, "" +
                "������ MaxValue ����������� ���������� �������� " +
                "������� ���������� ����������");
        }

        [Test(Description = "���������� ���� ������� MaxValue")]
        public void Test_MaxValue_Get_CorrectValue()
        {
            var expected = 20;
            var actual = _parameter.MaxValue;
            Assert.AreEqual(expected, actual, 
                "������ MaxValue ����������� ������������ ��������");
        }

        [Test(Description = "���������� ���� ������� MinValue")]
        public void Test_MinValue_Set_CorrectValue()
        {
            var expected = 20;
            _parameter.MinValue = expected;
            Assert.AreEqual(expected, _parameter.MinValue, 
                "������ MinValue ����������� ���������� ��������");
        }

        [Test(Description = "���������� ���� ������� MinValue")]
        public void Test_MinValue_Get_CorrectValue()
        {
            var expected = 10;
            var actual = _parameter.MinValue;
            Assert.AreEqual(expected, actual, 
                "������ MinValue ����������� ���������� ��������");
        }

        [Test(Description = "���������� ���� ������� Value")]
        public void Test_Value_Set_CorrectValue()
        {
            var expected = 15;
            _parameter.Value = expected;
            Assert.AreEqual(expected, _parameter.Value, 
                "������ Value ����������� ���������� ��������");
        }

        [Test(Description = "���������� ���� ������� Value")]
        public void Test_Value_Get_CorrectValue()
        {
            var expected = 10;
            var actual = _parameter.Value;
            Assert.AreEqual(expected, actual, 
                "������ Value ����������� ���������� ��������");
        }

        [Test(Description = "���� ������������ Parameter")]
        public void Test_Parameter_Designer()
        {
            string messege = "";
            var result = true;
            if(_parameter.MinValue != 10)
            {
                result = false;
                messege = "������ ��� �������� ������������ " +
                    "�������� ���������";
            }

            if (_parameter.MaxValue != 20)
            {
                result = false;
                messege = "������ ��� �������� ������������� " +
                    "�������� ���������";
            }

            if (_parameter.Value != 10)
            {
                result = false;
                messege = "������ ��� �������� �������� " +
                    "�������� ���������";
            }
            Assert.IsTrue(result, messege);
        }

        [TestCase("-100", 
            "������ ��������� ���������� ����, ������������ �������� ������ �������������",
           TestName = "���������� �������� ������ ������������")]
        [TestCase("100", 
            "������ ��������� ���������� ����, ������������ �������� ������ �������������",
           TestName = "���������� �������� ������ �������������")]
        public void TestLastModTimeSet_ArgumentException(string wrongLastModTime, string messege)
        {
            Assert.Throws<ArgumentException>(() => 
            { _parameter.Value = double.Parse(wrongLastModTime); }, messege);
        }
    }
}