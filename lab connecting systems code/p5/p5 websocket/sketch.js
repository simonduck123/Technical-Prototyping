let socketReady = false;
let socket; 

let x = 200;
let y = 200; 

function setup() {
  createCanvas(400, 400);
  initWebSocket(); 
}

function draw() {
  background(128);
  ellipse(x,y,10,10); 
}

function mouseMoved() {
  
  x = mouseX;
  y = mouseY;
  
  if (socketReady) {
    let coordinateObject = {
      "xPos": x, 
      "yPos": y,
    }

    let jsonString = JSON.stringify(coordinateObject);
  
    socket.send(jsonString);
  }
}

function wsIncomingMessage(e) {
  let jsonObj = JSON.parse(e.data);
  x = jsonObj.xPos;
  y = jsonObj.yPos;
}


function initWebSocket() {
  socket = new WebSocket('ws://localhost:8080/processing');

  socket.onopen = function () {
    socketReady = true;
    console.log("websocket opened");
  }
  
  socket.onclose = function () {
    socketReady = false;
    console.log("websocket closed");
    // set an interval to check if server became available
    // (this saves us a refresh)
    setTimeout(function() { initWebSocket() }, 5000);
  }

  socket.onmessage = wsIncomingMessage;
}

