let socketReady = false;
let socket; 
let mic;
let vol = 0;

let x = 200;
let y = 200; 


function setup() {
  createCanvas(400, 400);
  //let cnv = createCanvas(400, 400);
  //cnv.mousePressed(userStartAudio);
  initWebSocket(); 

  mic = new p5.AudioIn();
  mic.start();
}

function draw() {
  background(128);
  vol = mic.getLevel();
  let ellipseY = y - (vol * 450);
  ellipse(x, ellipseY, 10, 10);

    //socket.send(vol); //TURN ON FOR MIC
  
}

// sending mouse movement to processing
function mouseMoved() {
  
  x = mouseX;
  y = mouseY;
  
  if (socketReady) {
    let coordinateObject = {
      "xPos": x, 
      "yPos": y,
    }

    // let jsonString = JSON.stringify(coordinateObject);
    let CSVString = (x + "," + y);
    socket.send(CSVString); //TURN ON FOR MOUSE
  }
}

function wsIncomingMessage(e) {
  // let jsonObj = JSON.parse(e.data);
  // x = jsonObj.xPos;
  // y = jsonObj.yPos;

  let receivedString = e.data.split(",");
  console.log(receivedString);
    x = receivedString[0];
    y = receivedString[1];
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

