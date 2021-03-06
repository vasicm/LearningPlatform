package common;

import java.util.Map;

import org.neo4j.graphdb.Entity;

public class PropertyContainerHelper {
    public static void setValuesForKeys(final Entity entity, final String[] keyArray, final Map<String, Object> properties) {
	    for(String key: keyArray) {
            final Object value = properties.getOrDefault(key, null);
            if(value != null) {
                entity.setProperty(key, value);
            } else {
                entity.removeProperty(key);
            }
	    }
    }
}