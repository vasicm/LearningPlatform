mvn clean package

Copy-Item -Path .\target\vocabulary-booster-neo4j-procedure-4.2-1.0.0-SNAPSHOT.jar -Destination ..\..\..\..\docker\neo4j4\plugins\vocabulary-booster-neo4j-procedure-4.2-1.0.0-SNAPSHOT.jar
