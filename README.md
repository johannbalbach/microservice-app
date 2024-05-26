Репозиторий для выполнения задания по дисциплине Advanced Backend  
https://docs.google.com/document/d/1i4Q7iNdLtNwDvDZK8zDLleK7dQ1OMNDieZ6CjhoEfNQ


Для запуска необходимы RabbitMq, Docker Desktop, PostgreSQL.

Шаги:
1) Включите докер десктоп
2) Инициализируйте RabbitMQ:
откройте консоль от имени администратора и введите очередно:
docker run -d --hostname my-rabbit-host --name my-rabbit -e RABBITMQ_DEFAULT_USER=user -e RABBITMQ_DEFAULT_PASS=password rabbitmq:3-management

docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management

3) В файлах окружения для каждого микросервиса по пути 
Название микросервиса -> Core -> Application 
Добавьте строку соединения к бд:
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=EnrollmentDB;Username=;Password="
  },
4) Для работы микросервиса уведомлений(он работает, просто нигде не вызывается) нужна smtp яндекса(https://periodic-stretch-90c.notion.site/smtp-Yandex-Net-Web-Api-MailKit-80e2a3ff11b144558d02152124f7c0df)
Можете также использовать мои credentials:
  "EmailConfiguration": {
    "DisplayName": "Test Application",
    "From": "EnrollmentSystem505",
    "Host": "smtp.yandex.ru",
    "Password": "dbdvcbyrxakesoal",
    "Port": 465,
    "UserName": "EnrollmentSystem505@yandex.ru",
    "UseSSL": true,
    "UseStartTls": true,
    "UseOAuth": true
  },
5) В настройках solution выберите Multiple startup projects и поставьте все проекты с окончанием .Application, а также ApiGateway(я всё делал через сваггер, но он вроде работает без ошибок) и AdminPanel
