using System.Collections.ObjectModel;

namespace Collection.UnitTests
{
    public class CollectionTests
    {
      
        [Test]
        public void Test_Collection_EmptyConstructor()
        {
            //Arrange and Act
            var coll = new Collection<int>();
            //Assert
            Assert.AreEqual(coll.ToString(), "[]"); 
        }

        [Test]
        public void Test_Collection_ConstructorSingleItem()
        {
            //Arrange and Act
            var coll = new Collection<int>(5);
            //Assert
            Assert.AreEqual(coll.ToString(), "[5]");
        }

        [Test]
        public void Test_Collection_ConstructorMultipleItems()
        {
            //Arrange and Act
            var coll = new Collection<int>(5, 10);
            //Assert
            Assert.AreEqual(coll.ToString(), "[5, 10]");
        }

        [Test]
        public void Test_Collection_CountAndCapacity()
        {
            //Arrange and Act
            var coll = new Collection<int>(5, 10);
            //Assert
            Assert.AreEqual(coll.Count, 2);
            Assert.That(coll.Capacity, Is.GreaterThanOrEqualTo(coll.Count));
        }

        [Test]
        public void Test_Collection_Add()
        {
            //Arrange and Act
            var coll = new Collection<string>("Ivan", "John");
            //Assert

            coll.Add("Gosho");
            Assert.AreEqual(coll.ToString(), "[Ivan, John, Gosho]");
        }

        [Test]
        public void Test_Collection_GetByIndex()
        {
            var collection = new Collection<int>(5, 6, 7);
            var item = collection[1];

            Assert.That(item.ToString(), Is.EqualTo("6"));
        }

        [Test]
        public void Test_Collection_SetByIndex()
        {
            var collection = new Collection<int>(5, 6, 7);
            collection[1]= 666;

            Assert.That(collection.ToString(), Is.EqualTo("[5, 666, 7]"));
        }

        [Test]
        public void Test_Collection_GetByInvalidIndex()
        {
            var coll  = new Collection<string>("Ivan", "Peter");
            
            Assert.That(() => { var item = coll[2]; }, 
                Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_AddRangeWithGrow()
        {
            var nums = new Collection<int>();
            int oldCapacity = nums.Capacity;

            var newNums = Enumerable.Range(1000, 5000).ToArray();
            nums.AddRange(newNums);

            string expectedNums = "[" + string.Join(", ", newNums) + "]";

            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }

        [Test]
        public void Test_Collection_ToStringNestedCollections()
        {
            var names = new Collection<string>("Kate", "Nick");
            var nums = new Collection<int>(10, 100);
            var dates = new Collection<DateTime>();

            var nested = new Collection<object>(names, nums, dates);
            string nestedToString = nested.ToString();

            Assert.That(nestedToString, Is.EqualTo("[[Kate, Nick], [10, 100], []]"));
        }

        [Test]
        [Timeout(1000)]
        public void Test_Collection_1MilionItems()
        {
            const int itemsCount = 1000000;
            var nums = new Collection<int>();
            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());

            Assert.That(nums.Count == itemsCount);
            Assert.That(nums.Capacity >= nums.Count);
            for (int i = itemsCount-1; i >= 0; i--)
            {
                nums.RemoveAt(i);
            }
            Assert.That(nums.ToString() == "[]");
            Assert.That(nums.Capacity >= nums.Count);
        }

        [Test]
        public void Test_Collection_Clear()
        {
            var coll = new Collection<int>(10, 20, 30, 40);
            coll.Clear();

            Assert.That(coll, Is.Empty);
        }

        [Test]
        public void Test_Collection_AddRange()
        {
            var coll = new Collection<int>();
            int oldCapacity = coll.Capacity;

            var newNums = Enumerable.Range(1000, 5000).ToArray();
            coll.AddRange(newNums);

            Assert.That(coll.Capacity, Is.GreaterThan(oldCapacity));
        }

        [Test]
        public void Test_Collection_RemoveAll()
        {
                       
        }

        [Test]
        public void Test_Collection_RemoveAtStart()
        {
            var coll = new Collection<int>(10, 20, 100);
            coll.RemoveAt(0);

            Assert.That(coll.ToString(), Is.EqualTo("[20, 100]"));
        }

        [Test]
        public void Test_Collection_RemoveAtEnd()
        {
            var coll = new Collection<int>(10, 20, 100);
            coll.RemoveAt(2);

            Assert.That(coll.ToString(), Is.EqualTo("[10, 20]"));
        }
    }
}