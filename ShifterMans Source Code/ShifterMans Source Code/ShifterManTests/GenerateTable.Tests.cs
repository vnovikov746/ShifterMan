using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace GenerateTable.Tests
//{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void ShiftExistTest()
        {
            //arrange
            Shift s1 = new Shift("", "", "Sunday", "7", "15", "", "", "");
            ShiftTable t1 = new ShiftTable();
            t1.AddShift(s1);
            //act
            bool result = t1.shiftExists("Sunday", "7", "15");
            //assert
            Assert.AreEqual(result, true);
            
        }

        [TestMethod]
        public void ShiftExistNegativeTest()
        {
            //arrange
            Shift s1 = new Shift("", "", "Monday", "7", "15", "", "", "");
            ShiftTable t1 = new ShiftTable();
            t1.AddShift(s1);
            //act
            bool result = t1.shiftExists("Sunday", "7", "15");
            //assert
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void DivideLowOrHighTest()
        {
            //arrange
            Shift s1 = new Shift("", "", "Sunday", "7", "15", "", "High", "");
            Shift s2 = new Shift("", "", "Thursday", "23", "3", "", "Low", "");
            ShiftTable t1 = new ShiftTable();
            t1.AddShift(s1);
            t1.AddShift(s2);
            GenerateTable gen = new GenerateTable(t1, null);
            ShiftTable high = gen.getHighPriorityOptions();
            ShiftTable low = gen.getLowPriorityOptions();
            //act
            bool result_low = low.shiftExists("Thursday", "23", "3");
            bool result_high = high.shiftExists("Sunday", "7", "15");
            //assert
            Assert.AreEqual(result_low, true);
            Assert.AreEqual(result_high, true);

        }

        [TestMethod]
        public void DivideLowOrHighNegativeTest()
        {
            //arrange
            Shift s1 = new Shift("", "", "Sunday", "7", "15", "", "High", "");
            Shift s2 = new Shift("", "", "Thursday", "23", "3", "", "Low", "");
            ShiftTable t1 = new ShiftTable();
            t1.AddShift(s1);
            t1.AddShift(s2);
            //act
            GenerateTable gen = new GenerateTable(t1, null);
            ShiftTable high = gen.getHighPriorityOptions();
            ShiftTable low = gen.getLowPriorityOptions();
            bool result_low = low.shiftExists("Sunday", "7", "15");
            bool result_high = high.shiftExists("Thursday", "23", "3");
            //assert
            Assert.AreEqual(result_low, false);
            Assert.AreEqual(result_high, false);

        }

        [TestMethod]
        public void OptionLegalTest1_differentHour()
        {
            //arrange
            ShiftTable schedule = new ShiftTable();
            Shift shift = new Shift("123456789", "Polishuk", "Sunday", "7", "15", "Sion", "", "");
            schedule.AddShift(shift);
            Shift option = new Shift("123456789", "Polishuk", "Sunday", "23", "7", "Sion", "", "");
            //act
            bool result = schedule.optionLegal(option);
            //assert
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void OptionLegalTest2_differentID()
        {
            //arrange
            ShiftTable schedule = new ShiftTable();
            Shift shift = new Shift("123456789", "Polishuk", "Sunday", "7", "15", "Sion", "", "");
            schedule.AddShift(shift);
            Shift option = new Shift("111222333", "Polishuk", "Sunday", "7", "15", "Sion", "", "");
            //act
            bool result = schedule.optionLegal(option);
            //assert
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void OptionLegalNegativeTest_sameHour()
        {
            //arrange
            ShiftTable schedule = new ShiftTable();
            Shift shift = new Shift("123456789", "Polishuk", "Sunday", "7", "15", "Sion", "", "");
            schedule.AddShift(shift);
            Shift option = new Shift("123456789", "Polishuk", "Sunday", "7", "15", "Sion", "", "");
            //act
            bool result = schedule.optionLegal(option);
            //assert
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void GenerateScheduleSimpleTest1()
        {
            //arrange
            ShiftTable shifts = new ShiftTable();
            Shift shift = new Shift("", "", "Sunday", "7", "15", "Sion", "", "");
            shifts.AddShift(shift);

            ShiftTable options = new ShiftTable();
            Shift option = new Shift("123456789", "Polishuk", "Sunday", "7", "15", "Sion", "", "");
            options.AddShift(option);

            //act
            GenerateTable generator = new GenerateTable(options, shifts);
            ShiftTable schedule = generator.GenerateSchedule();

            //assert
            Assert.AreEqual(schedule.GetAllShifts().Count, 1);
            Assert.AreEqual(schedule.optionExists(option),true);

        }

        [TestMethod]
        public void GenerateScheduleSimpleTest2()
        {
            //arrange
            ShiftTable shifts = new ShiftTable();
            Shift shift1 = new Shift("", "", "Sunday", "7", "15", "Sion", "", "");
            Shift shift2 = new Shift("", "", "Sunday", "15", "23", "Sion", "", "");
            Shift shift3 = new Shift("", "", "Sunday", "23", "7", "Sion", "", "");
            shifts.AddShift(shift1);
            shifts.AddShift(shift2);
            shifts.AddShift(shift3);

            ShiftTable options = new ShiftTable();
            Shift option1 = new Shift("123456789", "Polishuk", "Sunday", "7", "15", "Sion", "High", "");
            Shift option2 = new Shift("123456789", "Polishuk", "Sunday", "15", "23", "Sion", "High", "");
            Shift option3 = new Shift("123456789", "Polishuk", "Sunday", "23", "7", "Sion", "High", "");
            options.AddShift(option1);
            options.AddShift(option2);
            options.AddShift(option3);

            //act
            GenerateTable generator = new GenerateTable(options, shifts);
            ShiftTable schedule = generator.GenerateSchedule();

            //assert
            Assert.AreEqual(schedule.GetAllShifts().Count, 2);
            Assert.AreEqual(schedule.optionExists(option1), true);
            Assert.AreEqual(schedule.optionExists(option3), true);

        }

        [TestMethod]
        public void GenerateSchedulePriorityTest()
        {
            //arrange
            ShiftTable shifts = new ShiftTable();
            Shift shift1 = new Shift("", "", "Sunday", "7", "15", "Sion", "", "");
            Shift shift2 = new Shift("", "", "Sunday", "15", "23", "Sion", "", "");
            Shift shift3 = new Shift("", "", "Sunday", "23", "7", "Sion", "", "");
            shifts.AddShift(shift1);
            shifts.AddShift(shift2);
            shifts.AddShift(shift3);

            ShiftTable options = new ShiftTable();
            Shift option1 = new Shift("123456789", "Polishuk", "Sunday", "7", "15", "Sion", "Low", "");
            Shift option2 = new Shift("123456789", "Polishuk", "Sunday", "15", "23", "Sion", "High", "");
            Shift option3 = new Shift("123456789", "Polishuk", "Sunday", "23", "7", "Sion", "Low", "");
            options.AddShift(option1);
            options.AddShift(option2);
            options.AddShift(option3);

            //act
            GenerateTable generator = new GenerateTable(options, shifts);
            ShiftTable schedule = generator.GenerateSchedule();

            //assert
            Assert.AreEqual(schedule.GetAllShifts().Count, 1);
            Assert.AreEqual(schedule.optionExists(option2), true);

        }

        [TestMethod]
        public void GenerateScheduleComplexTest()
        {
            //arrange
            ShiftTable shifts = new ShiftTable();
            Shift shift1 = new Shift("", "", "Sunday", "7", "15", "Sion", "", "");
            Shift shift2 = new Shift("", "", "Sunday", "15", "23", "Sion", "", "");
            Shift shift3 = new Shift("", "", "Sunday", "23", "7", "Sion", "", "");
            Shift shift4 = new Shift("", "", "Monday", "7", "15", "Sion", "", "");
            Shift shift5 = new Shift("", "", "Monday", "15", "23", "Sion", "", "");
            Shift shift6 = new Shift("", "", "Monday", "23", "7", "Sion", "", "");
            Shift shift7 = new Shift("", "", "Tuesday", "7", "15", "Sion", "", "");
            Shift shift8 = new Shift("", "", "Tuesday", "15", "23", "Sion", "", "");
            Shift shift9 = new Shift("", "", "Tuesday", "23", "7", "Sion", "", "");
            Shift shift10 = new Shift("", "", "Wednesday", "7", "15", "Sion", "", "");
            Shift shift11 = new Shift("", "", "Wednesday", "15", "23", "Sion", "", "");
            Shift shift12 = new Shift("", "", "Wednesday", "23", "7", "Sion", "", "");
            shifts.AddShift(shift1);
            shifts.AddShift(shift2);
            shifts.AddShift(shift3);
            shifts.AddShift(shift4);
            shifts.AddShift(shift5);
            shifts.AddShift(shift6);
            shifts.AddShift(shift7);
            shifts.AddShift(shift8);
            shifts.AddShift(shift9);
            shifts.AddShift(shift10);
            shifts.AddShift(shift11);
            shifts.AddShift(shift12);

            ShiftTable options = new ShiftTable();
            Shift option1 = new Shift("123456789", "Polishuk", "Sunday", "7", "15", "Sion", "Low", "");
            Shift option2 = new Shift("123456789", "Polishuk", "Sunday", "15", "23", "Sion", "High", "");
            Shift option3 = new Shift("123456789", "Polishuk", "Sunday", "23", "7", "Sion", "High", "");

            Shift option4 = new Shift("111222333", "Humi", "Sunday", "7", "15", "Sion", "High", "");
            Shift option5 = new Shift("111222333", "Humi", "Monday", "7", "15", "Sion", "High", "");
            Shift option6 = new Shift("111222333", "Humi", "Tuesday", "7", "15", "Sion", "High", "");

            Shift option7 = new Shift("222333444", "Soli", "Wednesday", "7", "23", "Sion", "High", "");
            Shift option8 = new Shift("222333444", "Soli", "Wednesday", "22", "7", "Sion", "High", "");
            Shift option9 = new Shift("222333444", "Soli", "Wednesday", "7", "7", "Sion", "High", "");

            Shift option10 = new Shift("333444555", "Kozo", "Wednesday", "7", "15", "Sion", "High", "");
            Shift option11 = new Shift("333444555", "Kozo", "Wednesday", "15", "23", "Sion", "High", "");
            Shift option12 = new Shift("333444555", "Kozo", "Wednesday", "23", "7", "Sion", "High", "");

            Shift option13 = new Shift("444555666", "Tkuma", "Monday", "7", "15", "Sion", "High", "");
            Shift option14 = new Shift("444555666", "Tkuma", "Monday", "15", "23", "Sion", "High", "");
            options.AddShift(option1);
            options.AddShift(option2);
            options.AddShift(option3);
            options.AddShift(option4);
            options.AddShift(option5);
            options.AddShift(option6);
            //options.AddShift(option7);
            options.AddShift(option8);
            //options.AddShift(option9);
            options.AddShift(option10);
            options.AddShift(option11);
            options.AddShift(option12);
            options.AddShift(option13);
            options.AddShift(option14);

            //act
            GenerateTable generator = new GenerateTable(options, shifts);
            ShiftTable schedule = generator.GenerateSchedule();

            //assert
            Assert.AreEqual(schedule.GetAllShifts().Count, 7);
            Assert.AreEqual(schedule.optionExists(option2), true);
            Assert.AreEqual(schedule.optionExists(option4), true);
            Assert.AreEqual(schedule.optionExists(option5), true);
            Assert.AreEqual(schedule.optionExists(option6), true);
            Assert.AreEqual(schedule.optionExists(option10), true);
            Assert.AreEqual(schedule.optionExists(option12), true);
            Assert.AreEqual(schedule.optionExists(option14), true);

        }

    }
//}
