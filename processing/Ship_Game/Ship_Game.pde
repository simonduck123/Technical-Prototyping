import oscP5.*;
import netP5.*;

OscP5 oscP5;
NetAddress myRemoteLocation;


void draw(){
  if(keyPressed){
    if(key == 'i' || key == 'I'){
     sendMessage("i"); 
    }
    else if(key == 'j' || key == 'J'){
     sendMessage("j"); 
    }
    else if(key == 'k' || key == 'K'){
     sendMessage("k"); 
    }
    else if(key == 'l' || key == 'L'){
     sendMessage("l"); 
    }
  }
  else{
  sendMessage("nothing");
  }
}

void mouseClicked()
{
  sendMessage("p");
}

void setup(){
  size(400,400);
  oscP5 = new OscP5(this,8000);
  myRemoteLocation = new NetAddress("127.0.0.1",9000);
}

void sendMessage(String letter) {
  OscMessage myMessage = new OscMessage("/game");
  myMessage.add(letter);
  oscP5.send(myMessage, myRemoteLocation); 
}
