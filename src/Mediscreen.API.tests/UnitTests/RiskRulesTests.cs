using MediscreenAPI.Model;
using Newtonsoft.Json;

namespace MediscreenAPI.tests.UnitTests
{
    public class RiskRulesTests
    {
        private RiskRules _riskRules;
        public RiskRulesTests()
        {
            string file = File.ReadAllText(@"D:\OpenClassroom\project-10\CodeBase\Mediscreen\src\Mediscreen.API.tests\UnitTests\TestRules.json");

            _riskRules = JsonConvert.DeserializeObject<RiskRules>(file);
        }

        [Fact]
        public void TestNoTriggerReturnNone()
        {
            //Arrange
            int age = 10;
            Sex sex = Sex.M;
            int triggers = 0;

            //Act
            string expected = "None";
            string actual = _riskRules.EvaluateRisk(age, sex, triggers);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestManyTriggerReturnOnSet()
        {
            //Arrange
            int age = 10;
            Sex sex = Sex.M;
            int triggers = 100;

            //Act
            string expected = "Early Onset";
            string actual = _riskRules.EvaluateRisk(age, sex, triggers);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestUnderFReturnOnSet()
        {
            //Arrange
            int age = 29;
            Sex sex = Sex.F;
            int triggers = 7;

            //Act
            string expected = "Early Onset";
            string actual = _riskRules.EvaluateRisk(age, sex, triggers);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestUnderFReturnInDanger()
        {
            //Arrange
            int age = 29;
            Sex sex = Sex.F;
            int triggers = 4;

            //Act
            string expected = "In Danger";
            string actual = _riskRules.EvaluateRisk(age, sex, triggers);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestOverReturnInDanger()
        {
            //Arrange
            int age = 40;
            Sex sex = Sex.F;
            int triggers = 7;

            //Act
            string expected = "In Danger";
            string actual = _riskRules.EvaluateRisk(age, sex, triggers);

            //Assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void TestOverReturnBorderline()
        {
            //Arrange
            int age = 40;
            Sex sex = Sex.F;
            int triggers = 2;

            //Act
            string expected = "Borderline";
            string actual = _riskRules.EvaluateRisk(age, sex, triggers);

            //Assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void TestOverMReturnBorderline()
        {
            //Arrange
            int age = 31;
            Sex sex = Sex.M;
            int triggers = 2;

            //Act
            string expected = "Borderline";
            string actual = _riskRules.EvaluateRisk(age, sex, triggers);

            //Assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void TestOneTriggerReturnNone()
        {
            //Arrange
            int age = 33;
            Sex sex = Sex.M;
            int triggers = 1;

            //Act
            string expected = "None";
            string actual = _riskRules.EvaluateRisk(age, sex, triggers);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}