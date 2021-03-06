package common;


public class Output {
    public String uuid;
    public long id;
    public boolean success;
    public boolean isNew;

    public Output(final String uuid, final long id) {
        this.uuid = uuid == null ? "00000000-0000-0000-0000-000000000000" : uuid;
        this.id = id;
        this.success = true;
    }

    public Output(final String uuid, final long id, boolean success) {
        this.uuid = uuid == null ? "00000000-0000-0000-0000-000000000000" : uuid;
        this.id = id;
        this.success = success;
    }
    
    public Output(final String uuid, final long id, boolean success, boolean isNew) {
        this.uuid = uuid == null ? "00000000-0000-0000-0000-000000000000" : uuid;
        this.id = id;
        this.success = success;
        this.isNew = isNew;
    }
}