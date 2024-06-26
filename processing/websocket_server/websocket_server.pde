// https://github.com/alexandrainst/processing_websockets
import websockets.*;
import oscP5.*;
import netP5.*;

OscP5 oscP5;
NetAddress myRemoteLocation;

WebsocketServer ws;
float x,y;
float ellipseY;
float moveBallUp;
float value;
String numberMessage;
int intMessage;
int colorSlider;
String activity = ""; 

JSONObject mouseCoords = new JSONObject();

void setup(){
  size(400,400);
  ws= new WebsocketServer(this,8080,"/processing");
  mouseCoords = new JSONObject();
    
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

void draw(){
  background(128);
  ellipse(x,y,10,10);
  sendMessage();
  //ellipseY y - (data * 450);
  //moveBallUp = ellipseY * 500
  //ellipse(x, moveBallUp, 10, 10);
  //println(moveBallUp);
  
}

void mouseMoved() {
  //x = mouseX;
  //y = mouseY;
  //mouseCoords.setInt("xPos", x);
  //mouseCoords.setInt("yPos", y);
  //ws.sendMessage(mouseCoords.toString());
  //ws.sendMessage(x + "," + y);
  
}

//void mouseClicked() {
//  sendMessage();
//}

//void mouseDragged() {
//  sendMessage();
//}

//Send OSC to Unity
void sendMessage() {
  
  activity = "sending: ";
  
  // normalize the value between 0.0 - 1.0; 
  //value = mouseX/float(width);
  float xValue = x/float(width);
  
  OscMessage myMessage = new OscMessage("/test");
  myMessage.add(xValue);
  //println(value);
  oscP5.send(myMessage, myRemoteLocation); 
}

//Receive mouse movement from P5
void webSocketServerEvent(String data){
  
  //println(data);
  
  // echo the message, so all connected clients will
  // receive it. 
  //ws.sendMessage(data);
 
  //ellipseY = Float.parseFloat(data);
  
 // parse the json object
 // and set the global x/y variables to change the ellipse position (in draw)
 //JSONObject json = parseJSONObject(data);
 //  x = json.getInt("xPos");
 //  y = json.getInt("yPos");
  float[] mouseCoordinates = float(split(data, ','));
  x = mouseCoordinates[0];
  y = mouseCoordinates[1];
}
