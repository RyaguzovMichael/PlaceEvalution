## Требуемые установленные программы

Для запуска приложения требуется Docker и Docker-compose  

## Запуск проекта

В папке с проектом (там где находится файл docker-compose.yml) выполняем к консоли команду:  
``
docker-compose up -d
``

## Доступ к проекту

Доступ к Swagger: http://localhost:5555/swagger/index.html  
Доступ к API напрямую: http://localhost:5555/api/ {путь к эндпоинту}  
Доступ к файлам загруженным на сервер: http://localhost:5555/data/ {имя файла}  