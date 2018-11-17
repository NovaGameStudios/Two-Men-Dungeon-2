using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CustomEventMethod<E> (E customEvent) where E : CustomEvent;

public static class CustomEventSystem {
	
//	public static Dictionary<Type, List<MethodInfo>> registeredHandlers = new Dictionary<Type, List<CustomEventMethod<CustomEvent>>> ();
//	public static Dictionary<Type, List<KeyValuePair<CustomListener, MethodInfo>>> registeredHandlers = new Dictionary<Type, List<KeyValuePair<CustomListener, MethodInfo>>> ();

	//                      <EventType, List<CustomListener>>
	public static Dictionary<Type,      List<CustomListener>> registeredListeners = new Dictionary<Type, List<CustomListener>> ();

	//                      <EventType, Dictionary<ListenerType, Handler   >> TODO MethoInfo[]
	public static Dictionary<Type,         Dictionary<Type,      MethodInfo>> handlerList = new Dictionary<Type, Dictionary<Type, MethodInfo>> ();

//	public static Dictionary<Type, List<CustomListener>> registeredListeners = new Dictionary<Type, List<CustomListener>> ();

//	public static List<CustomListener> registeredListeners = new List<CustomListener> ();

//	public static void subscribe<E> (this CustomListener listener) where E : CustomEvent {
//		Type eventType = typeof (E);
//		if (!registeredListeners.ContainsKey (eventType))
//			registeredListeners.Add (eventType, new List<CustomListener> ());
//		registeredListeners [eventType].Add (listener);
//	}

//	public static void registerHandler<E> (this CustomListener listener, CustomEventMethod<E> handler) where E : CustomEvent {
//		Type eventType = typeof (E);
//		if (!registeredHandlers.ContainsKey (eventType))
//			registeredHandlers.Add (eventType, new CustomEventMethod<CustomEvent> ());
//		registeredHandlers [eventType].Add (
//			new KeyValuePair<CustomListener, CustomEventMethod<CustomEvent>> (listener, handler as CustomEventMethod<CustomEvent>));
//	}
	public static void registerHandler<E> (this CustomListener listener, CustomEventMethod<E> handler) where E : CustomEvent {
		Type listenerType = listener.GetType ();
		Type eventType = typeof (E);
		Debug.Log ("Registering " + handler.Target + "." + handler.Method.Name + " for " + eventType);
		if (!handlerList.ContainsKey (eventType)) handlerList.Add (eventType, new Dictionary<Type, MethodInfo> ());
		if (handlerList [eventType].ContainsKey (listenerType)) return;
		handlerList [eventType].Add (listenerType, handler.Method);
		listener.register<E> ();

//		registeredHandlers [eventType].Add (new KeyValuePair<CustomListener, MethodInfo> (listener, handler.Method));
	}
	public static void register<E> (this CustomListener listener) where E : CustomEvent {
		Type eventType = typeof (E);
		if (!registeredListeners.ContainsKey (eventType)) registeredListeners.Add (eventType, new List<CustomListener> ());
		if (registeredListeners [eventType].Contains (listener)) return;
		registeredListeners [eventType].Add (listener);
	}
//	public static void register (this CustomListener listener) {
//		if (!registeredListeners.Contains (listener))
//			registeredListeners.Add (listener);
//	}
//	public static void subscribeAll<L> (L listener) where L : CustomListener {
//		listener.subscribe<L, CustomEvent> (listener, CustomEvent);
//	}

	public static void call<E> (this E customEvent) where E : CustomEvent {
		Debug.Log ("Event call: " + customEvent);
//		Type eventType = typeof (E);
//		if (!registeredListeners.ContainsKey (eventType)) return;
//		foreach (CustomListener listener in registeredListeners [eventType]) {
//			listener.dispatchToMethods (customEvent);
//		}

		Type eventType = typeof (E);
		if (!registeredListeners.ContainsKey (eventType) || !handlerList.ContainsKey (eventType)) return;
		Dictionary<Type, MethodInfo> eventHandlerList = handlerList [eventType];

//		foreach (KeyValuePair<CustomListener, CustomEventMethod<CustomEvent>> handler in registeredHandlers [eventType]) {
//			CustomListener listener = handler.Key;
//			CustomEventMethod<CustomEvent> method = handler.Value;
//			method.
//		}
//		Debug.Log ("Handlers: " + registeredHandlers [eventType].Count);
//		foreach (CustomEventMethod<CustomEvent> method in registeredHandlers [eventType]) {
//			if (method != null) method (customEvent);
//		}
		foreach (CustomListener listener in registeredListeners [eventType]) {
			Type listenerType = listener.GetType ();
			if (!eventHandlerList.ContainsKey (listenerType)) continue;
			MethodInfo method = eventHandlerList [listenerType];
			Debug.Log ("Invoking " + method.Name + " on " + listener + " for " + customEvent);
			method.Invoke (listener, new object[] {customEvent});
		}
//		if (eventType != typeof(CustomEvent)) callByType (customEvent, eventType.BaseType);
//		callByType (customEvent, typeof (E));
	}

//	public static void callByType (CustomEvent customEvent, Type eventType) {
//		if (!registeredListeners.ContainsKey (eventType)) return;
//		foreach (CustomListener listener in registeredListeners [eventType]) {
//			listener.dispatchToMethodsByType (customEvent, eventType);
//		}
//		if (eventType != typeof (CustomEvent)) callByType (customEvent, eventType.BaseType);
//	}


//	public static void dispatchToMethods<E> (this CustomListener listener, E customEvent) where E : CustomEvent {
//		Debug.Log ("Dispatching " + customEvent + " to " + listener);
//		foreach (MethodInfo method in CustomEventHandler.getHandlerMethods (listener.GetType (), typeof (E)))
//			method.Invoke (listener, new object[] {customEvent});
//	}
//	public static void dispatchToMethods<E> (object listener, E customEvent) where E : CustomEvent {
//		Debug.Log ("Dispatching " + customEvent + " to " + listener);
//		foreach (MethodInfo method in CustomEventHandler.getHandlerMethods ((listener.GetType (), typeof (E)))
//			method.Invoke (listener, new object[] {customEvent});
//		//		dispatchToMethodsByType (listener, customEvent, typeof (E));
//	}

//	public static void dispatchToMethodsByType (this CustomListener listener, CustomEvent customEvent, Type eventType) {
//		foreach (MethodInfo method in CustomEventHandler.getHandlerMethods (listener.GetType (), eventType))
//			method.Invoke (listener, new object[] {customEvent});
//	}

}
