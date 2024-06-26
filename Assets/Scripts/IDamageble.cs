using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageble<T>
{
     void Damage(T damageTaken);
}