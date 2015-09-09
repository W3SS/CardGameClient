using System.Collections.Generic;

public class EventDispatcher {
	List<IEventListener> _listeners = new List<IEventListener>();

	public void addEventListener(IEventListener listener) {
		_listeners.Add(listener);
	}

	public void removeEventListener(IEventListener listener) {
		_listeners.Remove(listener);
	}

	// 听说`params object[] list`比直接调用慢200倍，吓得不敢用了= =
	public void dispatchEvent(string eventName, object param) {
		foreach(IEventListener listener in _listeners) {
			listener.onReceiveEvent(eventName, param);
		}
	}
}