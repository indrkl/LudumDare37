using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dartTrap : TriggerTrap {
	public int xDirection;
	public int yDirection;

	public override void action ()
	{
		Dart dart = Instantiate (GameMaster.instance.dartPrefab);
		dart.transform.position = transform.position;
		dart.transform.position += new Vector3 (xDirection * 0.5f, yDirection * 0.5f, 0);
		dart.direction = xDirection;
		dart.yDirection = yDirection;
		dart.source = this.gameObject;
	}
}
