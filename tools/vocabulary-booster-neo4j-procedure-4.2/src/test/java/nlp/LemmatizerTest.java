package nlp;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.TestInstance;
import org.neo4j.driver.Config;
import org.neo4j.driver.Driver;
import org.neo4j.driver.GraphDatabase;
import org.neo4j.driver.Session;
import org.neo4j.harness.Neo4j;
import org.neo4j.harness.Neo4jBuilders;

import static org.assertj.core.api.Assertions.assertThat;

@TestInstance(TestInstance.Lifecycle.PER_CLASS)
public class LemmatizerTest {
    private static final Config driverConfig = Config.builder().withoutEncryption().build();
    private Neo4j embeddedDatabaseServer;

    @BeforeAll
    void initializeNeo4j() {
        this.embeddedDatabaseServer = Neo4jBuilders.newInProcessBuilder()
                .withDisabledServer()
                .withFunction(Lemmatizer.class)
                .build();
    }

    @Test
    void lematizer() {
        // This is in a try-block, to make sure we close the driver after the test
        try(Driver driver = GraphDatabase.driver(embeddedDatabaseServer.boltURI(), driverConfig);
            Session session = driver.session()) {

            // When
            String result = session.run( "RETURN nlp.lemma('better', 'adv') AS result").single().get("result").asString();
            assertThat( result).isEqualTo(( "well" ));
            result = session.run( "RETURN nlp.lemma('better', 'noun') AS result").single().get("result").asString();
            assertThat( result).isEqualTo(( "better" ));
        }
    }
}