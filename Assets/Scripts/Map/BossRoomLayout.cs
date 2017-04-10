using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Map {
	class BossRoomLayout : SimpleSquareRoomLayout {
		public BossRoomLayout(AreaFactory factory, int width, int height) : base(factory, width, height) {
			 
		}

		public override void AddEnemies(float difficulty) {
			//LayoutAtRandom(enemyPositions, (int)Mathf.Floor(difficulty * 3f), (int)Mathf.Floor(difficulty * 5f));
			LayoutAtRandom(enemyPositions, 1, 1);
		}
	}
}
