using NUnit.Framework;
using Payroll_Lib;
using System;
using System.Collections.Generic;

namespace PayrollTests
{
    [TestFixture]
    public class EmployeeTests
    {
        [Test]
        public void TestEmployeeProperties_SetAndGetValues()
        {
            var testCases = new List<(string firstName, string surname, int children, string department, string position, int workExperience, int income, double bonus)>
            {
                ("John", "Doe", 2, "IT", "Senior Developer", 5, 40000, 1000),
                ("Jane", "Smith", 1, "HR", "Junior Developer", 3, 30000, 500),
                ("Mark", "Johnson", 0, "Finance", "Mid-level Developer", 7, 50000, 2000),
                ("Anna", "Brown", 3, "Marketing", "IT Manager", 10, 60000, 1500)
            };

            foreach (var testCase in testCases)
            {
                try
                {
                    Employee employee = new Employee(testCase.firstName, testCase.surname, testCase.children, testCase.department, testCase.position, testCase.workExperience, testCase.income, testCase.bonus);

                    Assert.That(employee.FirstName, Is.EqualTo(testCase.firstName));
                    Assert.That(employee.Surname, Is.EqualTo(testCase.surname));
                    Assert.That(employee.Children, Is.EqualTo(testCase.children));
                    Assert.That(employee.Department, Is.EqualTo(testCase.department));
                    Assert.That(employee.Position, Is.EqualTo(testCase.position));
                    Assert.That(employee.WorkExperience, Is.EqualTo(testCase.workExperience));
                    Assert.That(employee.Income, Is.EqualTo(testCase.income));
                    Assert.That(employee.Bonus, Is.EqualTo(testCase.bonus));
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Test failed for input {testCase}: {ex.Message}");
                }
            }
        }

        [Test]
        public void TestWorkExperience_Limits()
        {
            var testCases = new List<(int inputExperience, int expectedExperience)>
            {
                (-5, 0),
                (10, 10),
                (40, 38)
            };

            foreach (var testCase in testCases)
            {
                try
                {
                    Employee employee = new Employee();
                    employee.WorkExperience = testCase.inputExperience;

                    Assert.That(employee.WorkExperience, Is.EqualTo(testCase.expectedExperience));
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Exception for input {testCase.inputExperience}: {ex.Message}");
                }
            }
        }

        [Test]
        public void TestIncomeAndBonus_SetAndGetValues()
        {
            var testCases = new List<(int income, double bonus)>
            {
                (50000, 5000),
                (30000, 1000),
                (70000, 2500),
                (45000, 1500)
            };

            foreach (var testCase in testCases)
            {
                try
                {
                    Employee employee = new Employee();
                    employee.Income = testCase.income;
                    employee.Bonus = testCase.bonus;

                    Assert.That(employee.Income, Is.EqualTo(testCase.income));
                    Assert.That(employee.Bonus, Is.EqualTo(testCase.bonus));
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Exception for input (Income: {testCase.income}, Bonus: {testCase.bonus}): {ex.Message}");
                }
            }
        }

        [Test]
        public void TestDepartmentAndPosition_SetAndGetValues()
        {
            var testCases = new List<(string department, string position)>
            {
                ("IT", "Senior Developer"),
                ("HR", "Junior Developer"),
                ("Finance", "Mid-level Developer"),
                ("Marketing", "IT Manager")
            };

            foreach (var testCase in testCases)
            {
                try
                {
                    Employee employee = new Employee();
                    employee.Department = testCase.department;
                    employee.Position = testCase.position;

                    Assert.That(employee.Department, Is.EqualTo(testCase.department));
                    Assert.That(employee.Position, Is.EqualTo(testCase.position));
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Exception for input (Department: {testCase.department}, Position: {testCase.position}): {ex.Message}");
                }
            }
        }
    }

    [TestFixture]
    public class PayrollLibTests
    {
        public static void print(string message)
        {
            Console.WriteLine(message);
        }

        [Test]
        public void TestCheckPhone()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();

            var testCases = new List<(string phone, bool isValid, string expectedCountry)>
            {
                ("+302101234567", true, "Greece"),
                ("+35799123456", true, "Cyprus"),
                ("+391234567890", true, "Italy"),
                ("+441234567890", true, "UK"),
                ("+1234567890", false, ""),
                ("00302101234567", true, "Greece"),
                ("0035799123456", true, "Cyprus"),
                ("00391234567890", true, "Italy"),
                ("00441234567890", true, "UK"),
                ("+491234567890", false, "")
            };

            foreach (var testCase in testCases)
            {
                try
                {
                    string country = "";
                    bool result = payroll.CheckPhone(testCase.phone, ref country);

                    Assert.That(result, Is.EqualTo(testCase.isValid));
                    Assert.That(country, Is.EqualTo(testCase.expectedCountry));
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Exception for phone {testCase.phone}: {ex.Message}");
                }
            }
        }

        [Test]
        public void TestCheckIBAN()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();

            var testCases = new List<(string iban, bool isValid, string expectedCountry)>
            {
                ("GR1601101250000000012300695", true, "Greece"),
                ("CY17002001280000001200527600", true, "Cyprus"),
                ("IT60X0542811101000000123456", true, "Italy"),
                ("GB29NWBK60161331926819", true, "United Kingdom"),
                ("XX00123456789012345678", false, ""),
                ("GR00INVALIDIBAN", false, ""),
                ("IT99INVALIDIBAN123", false, "")
            };

            foreach (var testCase in testCases)
            {
                try
                {
                    string country = "";
                    bool result = payroll.CheckIBAN(testCase.iban, ref country);

                    Assert.That(result, Is.EqualTo(testCase.isValid));
                    Assert.That(country, Is.EqualTo(testCase.expectedCountry));
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Exception for IBAN {testCase.iban}: {ex.Message}");
                }
            }
        }

        [Test]
        public void TestCheckZipCode()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();

            var testCases = new List<(int zipCode, bool isValid)>
            {
                (10100, true),
                (99999, false),
                (20100, true),
                (37011, true),
                (38068, true),
                (45000, false),
                (99998, false)
            };

            foreach (var testCase in testCases)
            {
                try
                {
                    bool result = payroll.CheckZipCode(testCase.zipCode);
                    Assert.That(result, Is.EqualTo(testCase.isValid));
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Exception for zip code {testCase.zipCode}: {ex.Message}");
                }
            }
        }

        [Test]
        public void TestCalculateSalary()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();

            var testCases = new List<(Employee employee, double expectedAnnualSalary, double expectedNetAnnualIncome, double expectedNetMonthIncome)>
            {
                (new Employee("John", "Doe", 0, "IT", "Junior Developer", 2, 0, 0), 14840, 12032, 859), // 2 xronia prohpiresia (1000+60)*14 = 14840
                (new Employee("Jane", "Smith", 0, "IT", "Mid-level Developer", 0, 0, 0), 21000, 16064, 1147),
                (new Employee("Mark", "Johnson", 0, "IT", "Senior Developer", 10, 0, 0), 36400, 25356, 1811),
                (new Employee("Linda", "Brown", 0, "IT", "IT Manager", 15, 0, 0), 70000, 41865, 2990)
            };

            foreach (var testCase in testCases)
            {
                try
                {
                    double annualGrossSalary = 0, netAnnualIncome = 0, netMonthIncome = 0, tax = 0, insurance = 0;
                    payroll.CalculateSalary(testCase.employee, ref annualGrossSalary, ref netAnnualIncome, ref netMonthIncome, ref tax, ref insurance);

                    Assert.That(annualGrossSalary, Is.EqualTo(testCase.expectedAnnualSalary).Within(5.0));
                    Assert.That(netAnnualIncome, Is.EqualTo(testCase.expectedNetAnnualIncome).Within(5.0));
                    Assert.That(netMonthIncome, Is.EqualTo(testCase.expectedNetMonthIncome).Within(5.0));
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Exception during salary calculation for {testCase.employee.Position}: {ex.Message}");
                }
            }
        }

        [Test]
        public void TestNumofEmployees()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();

            var employees = new Employee[]
            {
                new Employee("John", "Doe", 0, "Networks", "Junior Developer", 2, 30000, 0),
                new Employee("Jane", "Doe", 0, "Networks", "Mid-level Developer", 5, 50000, 0),
                new Employee("Mark", "Smith", 0, "Networks", "Junior Developer", 1, 25000, 0),
                new Employee("Anna", "Taylor", 0, "Networks", "Senior Developer", 8, 70000, 0)
            };

            var testCases = new List<(string position, int expectedCount)>
            {
                ("Junior Developer", 2),
                ("Mid-level Developer", 1),
                ("Senior Developer", 1),
                ("IT Manager", 0)
            };

            foreach (var testCase in testCases)
            {
                try
                {
                    int count = payroll.NumofEmployees(employees, testCase.position);
                    Assert.That(count, Is.EqualTo(testCase.expectedCount));
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Exception during count for position {testCase.position}: {ex.Message}");
                }
            }
        }

        [Test]
        public void TestGetBonus()
        {
            Payroll_Lib.Payroll_Lib payroll = new Payroll_Lib.Payroll_Lib();

            var employees = new Employee[]
                {
                    new Employee("John", "Doe", 0, "Networks", "Junior Developer", 2, 30000, 0),
                    new Employee("Jane", "Doe", 0, "Networks", "Mid-level Developer", 5, 50000, 0)
                };

            var testCases = new List<(string department, double incomeGoal, double bonusAmount, bool isBonusAwarded, double[] expectedBonuses)>
                {
                    ("Networks", 75000, 10000, true, new double[] { 3750, 6250 }),
                    ("Networks", 100000, 10000, false, new double[] { 0, 0 })
                };

            foreach (var testCase in testCases)
            {
                try
                {
                    bool result = payroll.GetBonus(ref employees, testCase.department, testCase.incomeGoal, testCase.bonusAmount);
                    Assert.That(result, Is.EqualTo(testCase.isBonusAwarded), $"Expected bonus awarded status: {testCase.isBonusAwarded}, but got {result}.");
                    if (!testCase.isBonusAwarded)
                        continue;
                    for (int i = 0; i < employees.Length; i++)
                    {
                        Assert.That(employees[i].Bonus, Is.EqualTo(testCase.expectedBonuses[i]).Within(0.01), $"Expected bonus for {employees[i].FirstName} was {testCase.expectedBonuses[i]}, but got {employees[i].Bonus}.");
                    }
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Exception during bonus calculation for department {testCase.department}: {ex.Message}");
                }
            }
        }

    }
}
