using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretBaseState
{
   public abstract void EnterState(TurretStateManager state, Transform centreRayOrigin);
   public abstract void UpdateState(TurretStateManager state);
   public abstract void OnCollisionEnter(TurretStateManager state);

   


}
