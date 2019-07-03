#include "ofApp.h"
#include <string>
//--------------------------------------------------------------
void ofApp::setup(){

	wchar_t charAppPath[256];
	size_t len = sizeof(charAppPath);
	GetModuleFileName(NULL, charAppPath, len);
	wstring ws(charAppPath);
	string appPath(ws.begin(), ws.end());
	appPath = appPath.substr(0, appPath.find_last_of("\\"));
	ofLogToFile(appPath + "\\Log_" + std::to_string(ofGetCurrentTime().getAsMilliseconds()) + ".txt", true);

	ofSetFrameRate(60);
	rect = ofRectangle{ 100, 200, 50, 100 };
	float minSpeed = 1.0;
	float maxSpeed = 30.0;
	float currentSpeed = floor(static_cast <float> (rand()) / (static_cast <float> (RAND_MAX / (maxSpeed - minSpeed))));
	if (currentSpeed < 1.0)
	{
		currentSpeed = minSpeed;
	}

	rotateZ.set("z", currentSpeed, minSpeed, maxSpeed);
	rotationZ = rotateZ;
}

//--------------------------------------------------------------
void ofApp::update(){
	rotationZ = rotationZ + rotateZ;
	//ofSetFrameRate(static_cast <int> (rand()) / (static_cast <int> (RAND_MAX / (100 - 1))));
}

//--------------------------------------------------------------
void ofApp::draw(){
	ofTranslate(ofGetWidth() / 2, ofGetHeight() / 2);

	int rotationsCount = static_cast<int>((rotationZ - rotateZ) / 360);
	int currentAngle = (rotationZ - rotateZ) - 360 * rotationsCount;
	int lastRenderingTime = renderTime - startRenderingTime;

	std::ostringstream oss;
	oss << "Current draw angle: " << currentAngle << "deg\n"
		<< "Full rotations count: " << rotationsCount << "\n"
		<< "Time between renderings: " << lastRenderingTime << " microseconds" << "\n";
	std::string infoMessage = oss.str();

	startRenderingTime = ofGetElapsedTimeMicros();
	ofDrawBitmapString(infoMessage, -ofGetWidth() * 0.48, -ofGetHeight() * 0.48);

	ofRotateZ(rotationZ);
	ofSetColor(255, 0, 0);
	ofRect(rect);
	renderTime = ofGetElapsedTimeMicros();

	ofLog() << "Rendering ID: " + std::to_string(renderID) << "; "
			<< "Current draw angle: " + std::to_string(currentAngle) << "; "
			<< "Full rotations count: " + std::to_string(rotationsCount) << "; "
			<< "Time between renderings: " + std::to_string(lastRenderingTime);

	renderID++;
}

//--------------------------------------------------------------
void ofApp::keyPressed(int key){

}

//--------------------------------------------------------------
void ofApp::keyReleased(int key){

}

//--------------------------------------------------------------
void ofApp::mouseMoved(int x, int y ){

}

//--------------------------------------------------------------
void ofApp::mouseDragged(int x, int y, int button){

}

//--------------------------------------------------------------
void ofApp::mousePressed(int x, int y, int button){

}

//--------------------------------------------------------------
void ofApp::mouseReleased(int x, int y, int button){

}

//--------------------------------------------------------------
void ofApp::mouseEntered(int x, int y){

}

//--------------------------------------------------------------
void ofApp::mouseExited(int x, int y){

}

//--------------------------------------------------------------
void ofApp::windowResized(int w, int h){

}

//--------------------------------------------------------------
void ofApp::gotMessage(ofMessage msg){

}

//--------------------------------------------------------------
void ofApp::dragEvent(ofDragInfo dragInfo){ 

}
