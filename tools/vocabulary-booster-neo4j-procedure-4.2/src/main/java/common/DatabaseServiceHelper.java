package common;

import java.util.Map;
import java.util.UUID;

import org.neo4j.graphdb.GraphDatabaseService;
import org.neo4j.graphdb.Label;
import org.neo4j.graphdb.Node;
import org.neo4j.graphdb.Relationship;
import org.neo4j.graphdb.RelationshipType;
import org.neo4j.graphdb.Transaction;

public class DatabaseServiceHelper {
    public static Node getOrCreateNode(final Transaction transaction, final Label label, final String key,
    final Object value) {
        Node node = transaction.findNode(label, key, value);

        if (node == null) {
            node = transaction.createNode(label);
            node.setProperty("uuid", UUID.randomUUID().toString());
            node.setProperty(key, value);
            node.setProperty("created", System.currentTimeMillis());
        }

        node.setProperty("modified", System.currentTimeMillis());
        return node;
    }

    public static Relationship addRelationshipIfNotExist(final Transaction transaction, Node startNode, Node endNode, RelationshipType relationshipType)
	{
		Relationship relationship = null;
		for(Relationship rel : startNode.getRelationships(relationshipType)) {
			if(rel.getEndNodeId() == endNode.getId()) {
				relationship = rel;
			}
		}

		if(relationship == null) {
			relationship = startNode.createRelationshipTo(endNode, relationshipType);
		}

		// The solution in a manner of functional programming
		// return StreamSupport.stream(db.getNodeById(startNodeId).getRelationships(relationshipType).spliterator(), false).filter(rel -> rel.getEndNodeId() == endNodeId).findAny().orElse(db.getNodeById(startNodeId).createRelationshipTo(db.getNodeById(endNodeId), relationshipType));
		return relationship;
	}
}