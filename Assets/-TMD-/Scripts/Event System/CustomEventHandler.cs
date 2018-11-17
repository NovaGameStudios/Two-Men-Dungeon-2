using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage (AttributeTargets.Method, AllowMultiple = false)]
public class CustomEventHandler : Attribute {

//	public static Dictionary<Type, MethodInfo[]> handlerMethods = new Dictionary<Type, MethodInfo[]> ();

//	public static MethodInfo[] getHandlerMethods (Type listenerType, Type eventType) {
//		Debug.Log ("Getting handler methods from " + listenerType + " for " + eventType);
//		if (!handlerMethods.ContainsKey (listenerType)) {
//			MethodInfo[] m1 = listenerType.GetMethods (); // BindingFlags.DeclaredOnly | BindingFlags.Instance | ...
//			Debug.Log ("m1: " + m1.Count ()); //             ^^ then (slowly) try to add some flags
//			//                                                      vvvv Problem vvvv
//			IEnumerable<MethodInfo> m2 = m1.Where (method => method.GetCustomAttributes<CustomEventHandler> (true).Count () > 0);
//			//                                                      ^^^^ Problem ^^^^
//			Debug.Log ("m2: " + m2.Count ());
//			IEnumerable<MethodInfo> m3 = m2.Where (method => method.GetParameters ().Count () == 1);
//			Debug.Log ("m3: " + m3.Count ());
//			handlerMethods.Add (listenerType, m3.ToArray ());
////			handlerMethods.Add (listenerType,
////				listenerType.GetMethods (BindingFlags.DeclaredOnly | BindingFlags.Instance)
////					.Where (method => method.GetCustomAttributes (typeof (CustomEventHandler), false).Count () > 0)
////					.Where (method => method.GetParameters ().Count () == 1)
////					.ToArray ());
//		}
//		return handlerMethods [listenerType]
//			.Where (method => method.GetParameters () [1].ParameterType == eventType
//				|| eventType.IsSubclassOf (method.GetParameters () [1].ParameterType))
//			.ToArray ();
//	}
}
