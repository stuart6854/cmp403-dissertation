using NUnit.Framework;
using stuartmillman.dissertation.goap;

namespace stuartmillman.dissertation.tests
{
    public class GStateTests
    {
        [Test]
        public void Contains()
        {
            GState state = new GState();

            Assert.False(state.Has("one"));

            state.Set("one", 1);
            Assert.True(state.Has("one"));

            state.Set("two", 2);
            state.Set("three", 3);
            Assert.True(state.Has("two"));

            state.Set("one", 2);
            Assert.True(state.Has("one"));
        }

        [Test]
        public void Set()
        {
            GState state = new GState();
            Assert.AreEqual(state.StateCount, 0);

            state.Set("one", 1);
            Assert.AreEqual(state.StateCount, 1);

            state.Set("two", 2);
            state.Set("three", 3);
            Assert.AreEqual(state.StateCount, 3);

            state.Set("two", 5);
            Assert.AreEqual(state.StateCount, 3);
        }

        [Test]
        public void Get()
        {
            GState state = new GState();
            {
                Assert.False(state.Get("one", out var one));
            }

            {
                state.Set("one", 1);
                Assert.True(state.Get("one", out var one));
                Assert.AreEqual((int) one, 1);
            }
            {
                state.Set("two", 2);
                state.Set("five", 5);
                Assert.True(state.Get("five", out var five));
                Assert.AreEqual((int) five, 5);
            }
        }

        [Test]
        public void Remove()
        {
            GState state = new GState();

            state.Set("one", 1);
            Assert.True(state.Has("one"));

            state.Set("two", 2);
            state.Set("three", 3);
            state.Remove("two");
            Assert.False(state.Has("two"));

            state.Remove("one");
            Assert.False(state.Has("one"));
        }
    }
}