using System;
using Common;
using FakeItEasy;

namespace Tests1.Common
{
	public class CollectionExtensionsTest
	{
        private class A
        {
            public int B { get; set; }
            public int C { get; set; }
        }

        public CollectionExtensionsTest()
		{

        }

        [Fact]
        public void UpdateOrAddByPropertyTest()
        {
            var a0 = new A() { B = 1, C = 2, };
            var a1 = new A() { B = 3, C = 4, };
            var a2 = new A() { B = 1, C = 40, };

            var list = new List<A>(new A[] { a0, a1 });

            list.UpdateOrAddByProperty(a2, "B");

            Assert.DoesNotContain(a0, list);
            Assert.Contains(a1, list);
            Assert.Contains(a2, list);
        }

    }
}

