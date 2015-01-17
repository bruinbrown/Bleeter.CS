# Bleeter.CS
  
Bleeter is a simple microblogging service built with Akka.Net used to demonstrate modelling applications with the actor model.
  
It requires Azure table storage for storing authentication info but development storage is fine and that's what it's configured to use at the minute.
  
I've not really had much chance to create much of a UI but it is usable, you just need to set the OAuth token in local storage and then it will allow you to use the API. I mostly demonstrated with Postman and used the UI to demonstrate the SignalR streaming
