using osu.Framework.Testing;

namespace Tetriosu.Game.Tests.Visual
{
    public abstract partial class TetriosuTestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new TetriosuTestSceneTestRunner();

        private partial class TetriosuTestSceneTestRunner : TetriosuGameBase, ITestSceneTestRunner
        {
            private TestSceneTestRunner.TestRunner runner;

            protected override void LoadAsyncComplete()
            {
                base.LoadAsyncComplete();
                Add(runner = new TestSceneTestRunner.TestRunner());
            }

            public void RunTestBlocking(TestScene test) => runner.RunTestBlocking(test);
        }
    }
}
