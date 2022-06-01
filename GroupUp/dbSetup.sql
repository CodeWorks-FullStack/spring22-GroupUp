CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';


CREATE TABLE IF NOT EXISTS groups(
  id INT NOT NULL AUTO_INCREMENT primary key,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name TEXT NOT NULL,
  description TEXT,
  image TEXT,
  isPrivate TINYINT DEFAULT 0,
  creatorId VARCHAR(255) NOT NULL,

  FOREIGN KEY (creatorId)
    REFERENCES accounts(id)
    ON DELETE CASCADE
)default charset utf8;



CREATE TABLE IF NOT EXISTS members(
  id INT NOT NULL AUTO_INCREMENT primary key,
  groupId INT NOT NULL,
  profileId VARCHAR(255) NOT NULL,

  FOREIGN KEY (groupId)
    REFERENCES groups(id)
    ON DELETE CASCADE,
  
  FOREIGN KEY (profileId)
    REFERENCES accounts(id)
    ON DELETE CASCADE
)default charset utf8;