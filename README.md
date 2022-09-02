# ClipboardEnterRemover 클립보드 엔터 제거 프로그램

* 아이콘 이미지
<img src="https://raw.githubusercontent.com/azusa0999/ClipboardEnterRemover/main/img/enter_1131.ico" />

* 화면모습

![prg_main](https://img1.daumcdn.net/thumb/R1280x0/?scode=mtistory2&fname=https%3A%2F%2Fblog.kakaocdn.net%2Fdn%2FvnWHH%2FbtrK6B9w0Ji%2FiuwbiZtbOuovqP0g1yGgv0%2Fimg.png)


## 프로그램의 목적
해당 프로그램은 논문 등 pdf 파일에서 카피할 때 행변환(엔터) 때문에 불편한 사용자를 위해 클립보드 내부에서 자동으로 행변환 값을 제거해주는 프로그램입니다.
저의 경우 영문 번역을 위해 pdf 파일에서 자주 복사해서 구글 번역기에 붙여넣기를 해왔는데, 이 pdf에 보이는 개행된대로 카피가 된 점이 너무 불편했습니다. 
이러면 번역도 제대로 되지 않기 때문에 따로 개행된 값들을 삭제했어야 했습니다. (델리트->삭제->델리트->삭제..(무한루프))
그래서 아예 클립보드 상에서 이를 제거해주면 어떨까 싶어서 만들게 되었습니다. 혹시 연구를 주로 하시는 분들에게 도움이 되지 않을까 싶네요. 

## 개발환경
이 프로그램은 다음과 같은 환경에서 작업되었습니다.
* 플랫폼 : .NET Core 6.0
* 운영체제 : Windows 10 x64
* IDE : Visual Studio 2022

## 사용방법
1. setup.exe으로 프로그램을 다운 받아 설치하거나 ClipboardEnterRemover.exe를 다운받아 클릭합니다.
2. 하단의 [시작하기] 버튼을 클릭하면 버튼 텍스트가 [중지]로 바뀌고 버튼 우측의 프로그래스바가 활성화되어 실행중임을 알립니다. 이때부터 클립보드에서 엔터값을 제거하기 시작합니다. 
3. 다시 [중지] 버튼을 클릭하면 클립보드 엔터값 제거 기능은 중지됩니다.
4. [중지] 버튼을 클릭 후 pdf 파일을 가져와 개행되어있는 문자열을 복사해본 후 다른 곳에 붙여넣기를 해보세요. 개행되어 있습니까?
5. 다시 [시작하기] 버튼을 클릭 후 다시 pdf 파일에서 동일한 문자열을 복하해보세요. 개행이 모두 제거된 것을 확인해볼 수 있습니다.

### 사용예시
우선 프로그램이 실행중이 아닌 상태일 때 PDF 문서에서 복사를 해봅시다. 참고로 예시로 나온 아래의 논문은 현재 읽고 있는 논문으로 İdiman, Ç. (2022)<sup>[1](#footnote_1)</sup>임을 밝혀둡니다.
![screensh](https://img1.daumcdn.net/thumb/R1280x0/?scode=mtistory2&fname=https%3A%2F%2Fblog.kakaocdn.net%2Fdn%2FmbfmO%2FbtrK6Q0dqN6%2FkFkgjClIF47poD8WFlTZBk%2Fimg.png)
이제 구글에 붙여넣으면.. 이런! 제길! 개행 값이 섞여있군요.
![screensh](https://img1.daumcdn.net/thumb/R1280x0/?scode=mtistory2&fname=https%3A%2F%2Fblog.kakaocdn.net%2Fdn%2FbpGqtC%2FbtrK91muyVM%2FwPTe3xt6vWM6uD5XOEhWFk%2Fimg.png)
이제 <시작하기>를 눌러 실행상태로 해두고 복사해봅시다.
![screensh](https://img1.daumcdn.net/thumb/R1280x0/?scode=mtistory2&fname=https%3A%2F%2Fblog.kakaocdn.net%2Fdn%2FbaSOCb%2FbtrLafdIsCp%2FEeIfk5Hu53LeyFw7fuhZC1%2Fimg.png)
여러분이 cntr+c를 누르면 윈도우 운영체제는 "클립보드"라는 공간에 해당 선택된 데이터(이미지든 아스키든)를 저장해둡니다. 그다음 cntr+v를 누르면 클립보드 최상위 영역의 데이터를 붙여넣기할 수 있게 하는 것입니다. 이 프로그램은 클립보드를 지켜보고 있다가 데이터가 들어오면 자동으로 엔터값들을 제거해서 다시 저장합니다. 그렇게 되었는지 확인해볼까요?
![screensh](https://img1.daumcdn.net/thumb/R1280x0/?scode=mtistory2&fname=https%3A%2F%2Fblog.kakaocdn.net%2Fdn%2Fci0vz0%2FbtrK56Cjgqp%2Fhu1K9KlqhG0N9gk8MdmNsK%2Fimg.png)
네. 잘 되어 있네요. 성공적입니다.

## 기타사항
* 엔터값 제거 규칙은 개행에 대한 아스키코드(CR, LF)와 함께 이 개행 코드 앞에 "-"가 있는 경우도 제거를 하고 있습니다. 영문 논문의 경우 한 단어가 끝나기 전에 개행이 될 때는 개행 전에 "-" 표시를 하고 있기 때문입니다. 이는 복사 붙여넣기 할 때 쓸데없는 데이터니까 지우도록 했습니다.
* 윈도우 프로그램의 헤더에 당연히 있을 종료버튼이 비활성화되어 있는 걸 알 수 있습니다. 종료하는 방법은 별도로 했습니다. 작업트레이(윈도우 하단바 우측 끝)에서 프로그램 아이콘<img src="https://raw.githubusercontent.com/azusa0999/ClipboardEnterRemover/main/img/enter_1131.ico" width="20" height="20" />에 마우스를 대고 우측 클릭하여 메뉴에서 "exit"를 통해 종료시켜주세요. 이렇게 한대는 이유가 있습니다. 이 프로그램은 백그라운드 프로그램으로써 역할을 하기 위해 이런 패턴을 선택했습니다. 원하지 않을 때는 잠시 꺼두고 계속 실행해두는 편이 여러모로 편리하겠지요. 그러나 논문 읽기 뿐만 아니라 멀티태스킹을 즐기시는 분에게는 불편할 수도 있겠네요.
* 프로그램을 최소화하면 자동으로 프로그램을 보여지지 않게 합니다. 다시 보이려면 역시 작업트레이로 가셔서 프로그램 아이콘<img src="https://raw.githubusercontent.com/azusa0999/ClipboardEnterRemover/main/img/enter_1131.ico" width="20" height="20" />에 마우스 우측 클릭하여 메뉴 "show"를 클릭하면 볼 수 있습니다.
* 윈도우 시작 시 자동시작하게 하는 기능은 귀찮아서 못햇씁니다. 혹시 필요하신 분이 있다면 윈도우 10에서 시작프로그램을 수동으로 설정하는 방법을 구글링 해보시기 바랍니다.

## 링크
* setup.exe 파일(v1.0.0) : [다운받기](https://github.com/azusa0999/ClipboardEnterRemover/releases/download/v1.0.0/Release.zip)
* setup.exe 파일(v1.0.1) : [다운받기](https://github.com/azusa0999/ClipboardEnterRemover/releases/download/v1.0.0/Release_v1.0.1.zip)
* 실행파일 모음 : [찾아가기](https://github.com/azusa0999/ClipboardEnterRemover/tree/main/bin/Release/net6.0-windows) (모든 파일을 다운로드 받고 실행하시면 됩니다)

## 푸념
* 아아.. 그동안 쌓아올린 (↓->삭제->↓->삭제..(무한루프)) 신공을 이젠 봉인해야 할 때가 된 것 같습니다.
* 연구를 주로 하시는 분들에게는 작은 도움이 되지 않을까 합니다. 잘 써주시면 좋겟네요. 
* 아이콘은 저작권이 오픈된 이미지를 썼는데 저기에 "X"를 끄셔볼까 하다가 아무리 그래도 '수정 후 재배포'는 저작권 문제가 복잡해서.. 걍 그대로 썼습니다. 엔터를 제거하는 프로그램인데 아이콘이 엔터키만 덜렁 있어서 "???" 하시는 분들에게는 사과드립니다(?)
* 근데 이거 몇 시간도 안되서 투다닥 만든거라.. 품질은 보장되지 않습니다...공짜가 다 그렇죠.. 본업이 있는 관계로 피드백을 받아도 응답속도가 꽤 느리겠지만.. 그래도 피드백을 해주신다면 매우 감사할 거 같습니다 :)

-끝-

<a name="footnote_1">1</a> : İdiman, Ç. (2022). Tributary World-Ecologies, Part II: The Mediterranean World and the Crisis. Journal of World-Systems Research, 28(2), 391-414.
