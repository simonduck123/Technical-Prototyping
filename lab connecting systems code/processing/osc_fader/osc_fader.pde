import oscP5.*;
import netP5.*;
  
OscP5 oscP5;
NetAddress myRemoteLocation;

// store incoming/outgoing value
float value;

// to display sending or receiving
String activity = ""; 

void setup() {
  size(400,100);
  /* start oscP5, listening for incoming messages at port 8000 */
  oscP5 = new OscP5(this,8000);
  
  /* myRemoteLocation is a NetAddress. a NetAddress takes 2 parameters,
   * an ip address and a port number. myRemoteLocation is used as parameter in
   * oscP5.send() when sending osc packets to another computer, device, 
   * application. 
   * we send in this application to port 9000.
   */
  myRemoteLocation = new NetAddress("127.0.0.1",9000);
}

void draw() {
  
  background(64);  
  
  // display the value
  rect(0,height/2,value*width,20);
  
  text(activity + value,10,20);
}

void mouseClicked() {
  sendMessage();
}

void mouseDragged() {
  sendMessage();
}

void sendMessage() {
  
  activity = "sending: ";
  
  // normalize the value between 0.0 - 1.0; 
  value = mouseX/float(width);
  
  OscMessage myMessage = new OscMessage("/fader");
  myMessage.add(value); 
  oscP5.send(myMessage, myRemoteLocation); 
}

/* incoming osc message are forwarded to the oscEvent method. */
//void oscEvent(OscMessage theOscMessage) {
  
//  // only react on this addresspattern
//  if(theOscMessage.checkAddrPattern("/fader")==true) {
     
//     // check if the value is a float
//     if(theOscMessage.checkTypetag("f")) {
//        activity = "receiving: "; 
       
//        value = theOscMessage.get(0).floatValue();
//     }
//  }
//}
