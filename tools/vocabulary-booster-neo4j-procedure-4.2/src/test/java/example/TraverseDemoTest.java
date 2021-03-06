package example;

import org.junit.jupiter.api.*;
import org.neo4j.driver.*;
import org.neo4j.driver.Record;
import org.neo4j.harness.Neo4j;
import org.neo4j.harness.Neo4jBuilders;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.StringWriter;
import java.util.List;
import java.util.stream.Collectors;

import static org.assertj.core.api.Assertions.assertThat;


@TestInstance(TestInstance.Lifecycle.PER_CLASS)
class TraverseDemoTest {

    private static final Config driverConfig = Config.builder().withoutEncryption().build();
    private Neo4j embeddedDatabaseServer;

    @BeforeAll
    void initializeNeo4j() throws IOException {

        var sw = new StringWriter();
        try (var in = new BufferedReader(new InputStreamReader(getClass().getResourceAsStream("/movie.cypher")))) {
            in.transferTo(sw);
            sw.flush();
        }

        this.embeddedDatabaseServer = Neo4jBuilders.newInProcessBuilder()
            .withProcedure(TraverseDemo.class)
            .withFixture(sw.toString())
            .build();
    }

    @Test
    void findKeanuReevesCoActors() {

        try(
                Driver driver = GraphDatabase.driver(embeddedDatabaseServer.boltURI(), driverConfig);
                Session session = driver.session()) {

            var names = session.run("match (keanu:Person {name:'Keanu Reeves'})-[*1..2]-(coactors:Person)\n" +
                    "with coactors.name as names order by names\n" +
                    "return distinct names").stream()
                    .map(r -> r.get("names"))
                    .map(Value::asString)
                    .collect(Collectors.toList());

            List<Record> records = session.run("call travers.findCoActors('Keanu Reeves')").list();

            List<String> coActorNames = records.stream()
                    .map(r -> r.get("node"))
                    .map(node -> node.get("name"))
                    .map(Value::asString)
                    .sorted()
                    .collect(Collectors.toList());
            assertThat(coActorNames.size()).isEqualTo(names.size());
            assertThat(coActorNames).containsAll(names);
        }
    }
}
