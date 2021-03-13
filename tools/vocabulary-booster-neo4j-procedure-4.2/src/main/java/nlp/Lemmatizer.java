package nlp;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import java.util.UUID;
import java.util.regex.Pattern;
import java.util.stream.Stream;

import org.neo4j.graphdb.GraphDatabaseService;
import org.neo4j.graphdb.Label;
import org.neo4j.graphdb.Node;
import org.neo4j.graphdb.RelationshipType;
import org.neo4j.graphdb.Transaction;
import org.neo4j.logging.Log;
import org.neo4j.procedure.Context;
import org.neo4j.procedure.Description;
import org.neo4j.procedure.Mode;
import org.neo4j.procedure.Name;
import org.neo4j.procedure.Procedure;
import org.neo4j.procedure.UserFunction;

import common.DatabaseServiceHelper;
import common.Output;

public class Lemmatizer {
    static final Label TEXT = Label.label("Text");
    static final Label WORD = Label.label("Word");
    static final RelationshipType HASWORD =  RelationshipType.withName("HAS_WORD");
    @Context
    public GraphDatabaseService db;

    @Context
    public Log log;
    // String COMMA_DELIMITER = ",";
    static Map<String, Map<String, String>> map;

    static {
        try {
            final InputStream lemmaLuStream = Lemmatizer.class.getClassLoader()
                    .getResourceAsStream("main/resources/lemma_lu.csv");
            try (BufferedReader br = new BufferedReader(new InputStreamReader(lemmaLuStream, "UTF-8"))) {
                String line;
                map = new HashMap<String, Map<String, String>>();
                while ((line = br.readLine()) != null) {
                    final String[] values = line.split(",");
                    if (!map.containsKey(values[0])) {
                        map.put(values[0], new HashMap<String, String>() {
                            {
                                put(values[1], values[2]);
                            }
                        });
                    } else {
                        map.get(values[0]).put(values[1], values[2]);
                    }
                }
            }
        } catch (final IOException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
    }

    @UserFunction()
    @Description("nlp.lemma('better', 'noun')")
    public String lemma(@Name("word") final String word, @Name("wordClass") final String wordClass) {
        if (map.containsKey(word)) {
            final var mdict = map.get(word);
            if(mdict.containsKey(wordClass)) {
                return mdict.get(wordClass);
            }
        }
        return word;
    }

    @Procedure(value = "nlp.lemmatize_text", mode = Mode.WRITE)
    @Description("")
    public Stream<Output> lemmatizeText(@Name("text") final String text) {
        try (Transaction transaction = db.beginTx()) {
            final Node textNode = transaction.createNode(TEXT);
            textNode.setProperty("uuid", UUID.randomUUID().toString());
            textNode.setProperty("content", text);
            
            final String[] words = text.toLowerCase().split("[^\\w\\']");
            
            for (final String word : words) {
                final ArrayList<String> lemmatizedWord = this.lemmatizeWord(word.trim());

                for (final String lemmWord : lemmatizedWord) {
                    if(!lemmWord.isEmpty()) {
                        final Node lemmWordNode = DatabaseServiceHelper.getOrCreateNode(transaction, WORD, "expression", lemmWord);
                        DatabaseServiceHelper.addRelationshipIfNotExist(transaction, textNode, lemmWordNode, HASWORD);
                    }
                }
            }

            var returnStream = Stream.of(new Output(textNode.getProperty("uuid").toString(), textNode.getId(), true));

            transaction.commit();
            return returnStream;
        }
    }

    private ArrayList<String> lemmatizeWord(final String word) {
        if (map.containsKey(word)) {
            final Map<String, String> mdict = map.get(word);
            return new ArrayList<String>(mdict.values());
        }
        return new ArrayList<String>() {
            {
                add(word);
            }
        };
    }
}