using UnityEngine;

namespace UseCases.Services.PoolService
{
	[DefaultExecutionOrder(-2), DisallowMultipleComponent]
	internal sealed class PoolInstaller : MonoBehaviour
	{
		[SerializeField] private PoolContainer[] pools = null;

		private void Awake()
		{
			for (int i = 0; i < pools.Length; i++)
				pools[i].Populate();
		}

		[System.Serializable]
		private struct PoolContainer
		{
			[SerializeField] private GameObject prefab;
			[SerializeField, Min(1)] private int startCount;

			public void Populate() =>
				prefab.Populate(startCount);
		}
	}
}
