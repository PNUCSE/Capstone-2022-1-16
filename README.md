# Capstone-2022-1-16

### 1. 프로젝트 소개

본 프로젝트는 부산대학교 2022년 전기 16조의 졸업과제입니다. 

주제는 **스마트폰을 이용한 HMD 사용자의 가상환경 상호작용 몰입 개선 방법 연구**입니다.

저희는 스마트폰 카메라를 이용하여 

**HMD Tracking**을 통해 스마트폰의 위치를 알아내고 

**Lip motion Tracking**을 통해 사용자 입술 움직임을 검출하였습니다.

그리고 이를 Unity 상에 구현함으로써

사용자들이 가상환경에 보다 더 몰입할 수 있게끔 돕는 방안에 대해 연구하였습니다.

### 2. 팀소개

저희 띵호와 팀의 팀원을 소개합니다.

**띵호와란?** 저희 이명호 지도교수님의 별명인 띵호에서 착안하여, **띵호와 함께 한다**는 뜻으로 지었습니다.

Kyoungchan Kang, kangkc09@pusan.ac.kr, Tcp communication & Unity 

Yerin Park, diluny2@naver.com, Lip motion Deep Learning

Jinseo Kim, rlawlstj1103@pusan.ac.kr, HMD Tracking

### 3. 시스템 구성도

![졸업과제 시스템 구성도](https://user-images.githubusercontent.com/63496777/195577273-1105c23b-5138-4fbd-a883-6257a9e2e225.png)

### 4. 소개 및 시연 영상


### 5. 설치 및 사용법

**0. 파일 다운로드**

  - git clone을 통하여 repository의 파일들을 다운받습니다. (PythonLipYOLOv7 directory만 다운받아도 무관)

  - 아래의 게시된 링크로 두개의 unity package를 다운받습니다. (용량문제로 github에 업로드 불가)
  https://drive.google.com/file/d/19dLWRu9y50oECUQIeGrM9GHKKwwIGAML/view?usp=sharing
  : UnityARCameraApp package
  https://drive.google.com/file/d/1sUvXc_zr1jzK_9OQmeaek8ktdZeQg5DT/view?usp=sharing
  : UnityVRProject package 

<br>

**1. 환경설정**

- Python 환경설정

  아래 command를 python terminal에 실행하여 필요한 python package들을 다운받아야 합니다.
```
$ pip install -r requirements.txt
```

- Unity 환경설정

  Unity 프로젝트가 두개이므로 두 개의 프로젝트를 생성해야합니다.
  
  
  먼저 UnityARCameraApp이라는 **3D unity프로젝트를 생성**한 후, 상단 메뉴 탭에 **Assets->Import package->Custom package...** 를 클릭하여
  UnityARCameraApp/UnityARCameraApp.unitypackage파일을 선택하면 프로젝트 업로드가 완료됩니다.
  
  
  그 다음 UnityVRProject라는 **3D unity 프로젝트를 생성**한 후, 같은 방법으로 UnityVRProject/UnityVRProject.unitypackage를 Import package하시면 프로젝트 업로드가 됩니다.
  
<br>
  
**2. IP 설정**
  
   **※ 반드시 스마트폰, 데스크탑(or 노트북), VR기기가 같은 wifi에 연결되어 있어야 합니다.**

  - PythonLipYOLOv7/server/TCP_server.py에서 Line 59에 파이썬을 구동시키는 데스크탑의 ip 주소를 입력하면 됩니다.
```
server = ServerSocket('ip 주소', 50000)
```
  
  - PythonLipYOLOv7/yolov7-main/modified_detect.py에서 Line 21에 VR 기기의 ip 주소를 입력하면 됩니다.
```
host = 'ip 주소'
```

<br>

**3. Unity application 빌드**

  - **UnityARCameraApp 프로젝트에서 스마트폰을 연결**하고 상단 메뉴 탭에 File->Build Settings... 에서 Run Device에 해당 스마트폰이 연결되었는지 확인한 후
  Build and run을 합니다.
  
  - **UnityVRProject 프로젝트에서 VR 기기를 연결**하고 상단 메뉴 탭에 File->Build Settings... 에서 Run Device에 VR 기기가 연결되었는지 확인한 후 Build and run을 합니다.
  
  - Oculus quest 2의 경우 상단 메뉴 탭에서 **Oculus->Platform->Edit Settings**에 해당 기기의 시리얼 키를 입력해야 합니다.
  
<br>

**4. 구동 순서**

  * **PythonLipYOLOv7/server/TCP_server.py** 를 실행합니다.
  
  * **VR 기기를 켜서 앱을 구동**합니다.
  
  * **스마트폰 앱을 켜서 데스크탑 ip 주소와 VR 기기의 ip 주소를 입력**하고 start 버튼을 누릅니다.
  
  * **PythonLipYOLOv7/yolov7-main/runs_fix.py**를 실행합니다.
  
