using NUnit.Framework;
using GameArki.BufferIO.Sample;

namespace GameArki.BufferIO.Tests {

    public class BufferIOExtraSampleTest {

        [Test]
        public void TestRun() {

            MyModel myModel = new MyModel();
            myModel.charValue = 'D';
            myModel.byteValue = 5;
            myModel.sbyteValue = -85;
            myModel.shortValue = -88;
            myModel.ushortValue = 442;
            myModel.intValue = -32131;
            myModel.uintValue = 9988888;
            myModel.longValue = -999991;
            myModel.ulongValue = 11122333;
            myModel.floatValue = -1.0000003f;
            myModel.doubleValue = 4541.12321333f;
            myModel.strValue = "hello worlddd";

            myModel.byteArr = new byte[3] { 1, 2, 9 };
            myModel.sbyteArr = new sbyte[4] { -1, -1, -2, -8 };
            myModel.ushortArr = new ushort[1] { 222 };
            myModel.strArr = new string[2] { "hl", "ddd" };
            myModel.herModel = new HerModel() { name = "yo", value = 3 };
            myModel.herModelArr = new HerModel[2] {
                new HerModel{ name ="ho112", value = 4},
                new HerModel{ value = -99}
            };
            myModel.otherStr = "endall";

            byte[] data = new byte[2048];
            int offset = 0;
            myModel.WriteTo(data, ref offset);

            MyModel newModel = new MyModel();
            offset = 0;
            newModel.FromBytes(data, ref offset);

            Assert.That(newModel.charValue, Is.EqualTo('D'));
            Assert.That(newModel.byteValue, Is.EqualTo(5));
            Assert.That(newModel.sbyteValue, Is.EqualTo(-85));
            Assert.That(newModel.shortValue, Is.EqualTo(-88));
            Assert.That(newModel.ushortValue, Is.EqualTo(442));
            Assert.That(newModel.intValue, Is.EqualTo(-32131));
            Assert.That(newModel.uintValue, Is.EqualTo(9988888));
            Assert.That(newModel.longValue, Is.EqualTo(-999991));
            Assert.That(newModel.ulongValue, Is.EqualTo(11122333));
            Assert.That(newModel.floatValue, Is.EqualTo(-1.0000003f));
            Assert.That(newModel.doubleValue, Is.EqualTo(4541.12321333f));
            Assert.That(newModel.strValue, Is.EqualTo("hello worlddd"));

            Assert.That(newModel.byteArr.Length, Is.EqualTo(3));
            Assert.That(newModel.byteArr[0], Is.EqualTo(1));
            Assert.That(newModel.byteArr[1], Is.EqualTo(2));
            Assert.That(newModel.byteArr[2], Is.EqualTo(9));

            Assert.That(newModel.sbyteArr.Length, Is.EqualTo(4));
            Assert.That(newModel.sbyteArr[0], Is.EqualTo(-1));
            Assert.That(newModel.sbyteArr[1], Is.EqualTo(-1));
            Assert.That(newModel.sbyteArr[2], Is.EqualTo(-2));
            Assert.That(newModel.sbyteArr[3], Is.EqualTo(-8));

            Assert.That(newModel.ushortArr.Length, Is.EqualTo(1));
            Assert.That(newModel.ushortArr[0], Is.EqualTo(222));

            Assert.That(newModel.shortArr.Length, Is.EqualTo(0));

            Assert.That(newModel.strArr.Length, Is.EqualTo(2));
            Assert.That(newModel.strArr[0], Is.EqualTo("hl"));
            Assert.That(newModel.strArr[1], Is.EqualTo("ddd"));

            Assert.That(newModel.herModel, Is.Not.Null);
            Assert.That(newModel.herModel.value, Is.EqualTo(3));

            Assert.That(newModel.herModelArr.Length, Is.EqualTo(2));
            Assert.That(newModel.herModelArr[0].value, Is.EqualTo(4));
            Assert.That(newModel.herModelArr[1].value, Is.EqualTo(-99));

            Assert.That(newModel.otherStr, Is.EqualTo("endall"));

        }

    }
}