import oscP5.*;
import netP5.*;
  
OscP5 oscP5;
NetAddress myRemoteLocation;

// store incoming/outgoing value
float value;
String numberMessage;
int intMessage;
int colorSlider;
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
  fill(colorSlider);
  rect(0,height/2,value*width,20);
  
  fill(#FFFFFF);
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
  
  OscMessage myMessage = new OscMessage("/test");
  myMessage.add(value);
  //println(value);
  oscP5.send(myMessage, myRemoteLocation); 
}

/* incoming osc message are forwarded to the oscEvent method. */
void oscEvent(OscMessage theOscMessage) {
  
  // only react on this addresspattern
  if(theOscMessage.checkAddrPattern("/test2")==true) {
    
    //numberMessage = theOscMessage.toString();
    //println(numberMessage);
    //int message = Integer.parseInt(numberMessage);
     
     // check if the value is a float
     if(theOscMessage.checkTypetag("f")) {
        activity = "receiving: "; 
       
        value = theOscMessage.get(0).floatValue();
        //println(value);
        
        if(value == 1.0f){
          colorSlider = #FF0000;
        }else if(value == 2.0f){
         colorSlider = #08FF11; 
        }else if(value == 3.0f){
         colorSlider = #0822FF; 
         
        }
     }
  }
}
