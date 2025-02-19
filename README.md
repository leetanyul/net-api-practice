# net-api-practice
1. 코딩 스타일 공유용 프로젝트 입니다.
2. .net 8, automapper, entityframework, jwt
3. 프로젝트 라이프 사이클
- 미들웨어(edgemiddleware -> JwtMiddleware) 
-> provider(CustomAuthorizationPolicyProvider) 신규 청책일 경우 
-> filter(PermissionHandler) 
-> controller 
-> domain 
-> application 
-> infrastructure
4. 권한 체크방식 (세부적인 카테고리별 권한을 체크할 수 있도록 하기 위해 정책등록이 동적으로 가능하도록 구현함)
- jwt bearer 방식으로 유저 정보를 저장(유저 로그인은 외부 공통 로그인에서 로그인해서 정보를 가져오는 것으로 가정함)
- JwtMiddleware 에서는 token 시간 만료를 체크
- [Authorize(Policy = "none")] 일 경우 로그인된 상태이면 추가적인 권한 체크를 하지 않음
- [Authorize(Policy = "sample:r|account:r")] 일 경우 acccount 의 read 혹의 sample 의 read 권한이 있을 경우 접근 가능
- 권한 어트리뷰트를 달지 않을 경우 로그인되지 않은 상태에서 접근 가능
