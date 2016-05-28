console.log("Loaded");
var net = require('net');
var sockets = [];

function random(low, high) {
    return Math.random() * (high - low) + low;
}

function getHex(dec) {
    var hexArray = new Array("0", "1", "2", "3", 
                              "4", "5", "6", "7",
                              "8", "9", "A", "B", 
                              "C", "D", "E", "F");
    
    var code1 = Math.floor(dec / 16);
    var code2 = dec - code1 * 16;
    
    var decToHex = hexArray[code2];
    
    return (decToHex);
} 

/*
 * Callback method executed when data is received from a socket
 */
function receiveData(data) {
	var result = "";
	var dataReceived = data.toString().replace(/\r?\n|\r/g, "");
	console.log("Received '" + dataReceived + "'");
	switch (dataReceived) {
		
		case "ATZ":
			result = "ELM v1.5";
			break;
		case "ATSP0":
			result = "";
			break;		
		case "010D":
			result = "41 0D 3F";
			break;
		
		// Engine RPM
		case "010C":
			result = "41 0C 54 1B";
			break;
		
		// Distance KM
        case "0121":
            segment1 = Math.floor(Math.random() * 255);
            segment2 = Math.floor(Math.random() * 255);

			result = "41 21 " + getHex(segment1) +" " + getHex(segment2);
			break;
		
		// Distance KM
        case "0131":
            segment1 = Math.floor(Math.random() * 255);
            segment2 = Math.floor(Math.random() * 255);
            result = "41 31 " + getHex(segment1) + " " + getHex(segment2);
            break;
		case "ATZ0131":
			result= "00";
		default:
			console.log("ERROR");
			break;
	}
	console.log(result);
	for (var i = 0; i < sockets.length; i++) {
		if (sockets[i]._handle)
			sockets[i].write(result + ">");
	}
}

/*
 * Callback method executed when a new TCP socket is opened.
 */
function newSocket(socket) {
	console.log("Start Listening");
    sockets.push(socket);
    socket.on("error", function (err) {
        console.log("Caught flash policy server socket error: ")
    }
);

    socket.on('Exception', function (data) {
        console.log(data);
    });
	socket.on('data', function (data) {
		receiveData(data);
	})
}

// Create a new server and provide a callback for when a connection occurs
var server = net.createServer(newSocket);
server.listen(36000);


