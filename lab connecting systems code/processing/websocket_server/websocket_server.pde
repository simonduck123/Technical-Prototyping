// https://github.com/alexandrainst/processing_websockets
import websockets.*;

WebsocketServer ws;
int x,y;

JSONObject mouseCoords = new JSONObject();

void setup(){
  size(400,400);
  ws= new WebsocketServer(this,8080,"/processing");
  mouseCoords = new JSONObject();
}

void draw(){
  background(128);
  ellipse(x,y,10,10); 
}

void mouseMoved() {
  x = mouseX;
  y = mouseY;
  mouseCoords.setInt("xPos", x);
  mouseCoords.setInt("yPos", y);
  ws.sendMessage(mouseCoords.toString());
}

void webSocketServerEvent(String data){
  
  //println(data);
  
  // echo the message, so all connected clients will
  // receive it. 
  ws.sendMessage(data);
 
 // parse the json object
 // and set the global x/y variables to change the ellipse position (in draw)
 JSONObject json = parseJSONObject(data);
   x = json.getInt("xPos");
   y = json.getInt("yPos");

}
