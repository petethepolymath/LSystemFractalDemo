using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ECS_System {

	void InjectEntityPool (EntityPool entityPoolToInject);

}
