using System;


public interface Observable
{
	void Register(Observer o);
	void Deregister(Observer o);
}

