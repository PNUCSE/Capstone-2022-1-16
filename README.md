# Capstone-2022-1-16

### 1. 프로젝트 소개

부산대학교 전기 졸업과제로
**스마트폰을 이용한 HMD 사용자의 가상환경 상호작용 몰입 개선 방법 연구**
를 진행하였습니다.

저희는 **스마트폰 카메라**를 이용하며

**HMD Tracking**을 통해 **스마트폰의 위치**를 알아내고

**Lip motion Tracking**을 통해 사용자 **입술 움직임**을 검출하여

**Unity** 상에 구현함으로써

사용자들이 **가상환경에 보다 더 몰입할 수 있는 방법**을 연구하는 것

이것이 저희의 개발 목표였습니다.

<br>

### 2. 팀소개

저희는 띵호와 팀의 팀원을 소개합니다.

**띵호와란?** 저희 이명호 지도교수님의 별명인 띵호에서 착안하여, **띵호와 함께 한다**는 뜻으로 지었습니다.

Kyoungchan Kang, kangkc09@pusan.ac.kr, Tcp communication & Unity 

Yerin Park, diluny2@naver.com, Lip motion Deep Learning

Jinseo Kim, rlawlstj1103@pusan.ac.kr, HMD Tracking

<br>

### 3. 시스템 구성도

![졸업과제 시스템 구성도](https://user-images.githubusercontent.com/63496777/195577273-1105c23b-5138-4fbd-a883-6257a9e2e225.png)

<br>

### 4. 소개 및 시연 영상

<br>

### 5. 설치 및 사용법

0. 파일 다운로드

  git clone을 통하여 repository의 파일들을 다운받습니다.

1. 환경설정

1.1. python에 대한 환경설정.

  아래 command를 python terminal에 실행하여 환경설정합니다.
```
$ pip install -r requirements.txt
```

1.2. unity에 대한 환경설정.

  unity 프로젝트가 두개이므로 두 개의 프로젝트를 생성해야합니다.
  먼저 UnityARCameraApp(이름 달라도 무관)이라는 3D unity프로젝트를 생성한 후, 상단 메뉴 탭에 Assets->Import package->Custom package... 를 클릭하여
  UnityARCameraApp/UnityARCameraApp.unitypackage파일을 선택하면 프로젝트 업로드가 완료됩니다.
  그 다음 UnityVRProject라는 3D unity 프로젝트를 생성한 후, 같은 방법으로 UnityVRProject->UnityVRProject.unitypackage를 Import package하시면 프로젝트 업로드가 됩니다.
  
2. IP 설정
  
  ***스마트폰, 데스크탑(or 노트북), VR기기가 같은 wifi에 연결되어 있어야 합니다.***

  PythonLipYOLOv7/server/TCP_server.py에서 Line 59에 파이썬을 구동시키는 ip주소를 입력하면 됩니다.
```
server = ServerSocket('ip 주소', 50000)
```
  
  PythonLipYOLOv7/yolov7-main/modified_detect.py에서 Line 21에 VR 기기의 ip주소를 입력하면 됩니다.
```
host = 'ip 주소'
```

3. unity application 빌드

  3.1 UnityARCameraApp 프로젝트에서 스마트폰을 연결하고 상단 메뉴 탭에 File->Build Settings... 에서 Run Device에 해당 스마트폰이 연결되었는지 확인한 후
  Build and run을 합니다.
  
  3.2 UnityVRProject 프로젝트에서 VR기기를 연결하고 상단 메뉴 탭에 File->Build Settings... 에서 Run Device에 VR 기기가 연결되었는지 확인한 후 Build and run을 합니다.
  
  Oculus quest 2의 경우 상단 메뉴 탭에서 Oculus->Platform->Edit Settings에 해당 기기의 시리얼 키를 입력해야 합니다.
  
4. 구동 순서

  4.1 PythonLipYOLOv7/server/TCP_server.py 를 실행합니다.
  
  4.2 VR기기를 켜서 
  
