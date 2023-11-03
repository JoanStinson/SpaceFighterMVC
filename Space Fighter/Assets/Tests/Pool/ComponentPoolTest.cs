using UnityEngine;
using NUnit.Framework;
using JGM.Game;
using UnityEngine.TestTools;

namespace JGM.GameTests
{
    public class ComponentPoolTest
    {
        private ComponentPool<TestComponent> m_componentPool;
        private Transform m_mockPoolParent;

        public class TestComponent : MonoBehaviour { }

        [SetUp]
        public void SetUp()
        {
            m_componentPool = new ComponentPool<TestComponent>(5, m_mockPoolParent);
            m_mockPoolParent = new GameObject().transform;
        }

        [Test]
        public void When_Get_Expect_ReturnPooledComponent()
        {
            var pooledComponent = m_componentPool.Get();

            Assert.IsNotNull(pooledComponent);
            Assert.IsTrue(pooledComponent.gameObject.activeSelf);
        }

        [Test]
        public void When_Return_Expect_ComponentReturnedToPool()
        {
            var pooledComponent = m_componentPool.Get();
            m_componentPool.Return(pooledComponent);

            Assert.IsFalse(pooledComponent.gameObject.activeSelf);
        }

        [Test]
        public void When_GetFromEmptyPool_Expect_WarningAndNullComponent()
        {
            for (int i = 0; i < 5; i++)
            {
                m_componentPool.Get();
            }

            var component = m_componentPool.Get();

            Assert.IsNull(component);
            LogAssert.Expect(LogType.Warning, "ComponentPool<JGM.GameTests.ComponentPoolTest+TestComponent> is empty. All pooled components are in use.");
        }
    }
}