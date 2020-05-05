<?php
header('Refresh: 18000');
session_start();

require 'Meli/meli.php';
require 'configApp.php';
require_once 'Meli/Connection.php';

$meli = new Meli($appId, $secretKey);

if(isset($_GET['code']) || isset($_SESSION['access_token'])) {
	if(isset($_GET['code']) && !isset($_SESSION['access_token'])) {
		try {
			$user = $meli->authorize($_GET["code"], $redirectURI);
			$_SESSION['access_token'] = $user['body']->access_token;
			$_SESSION['expires_in'] = time() + $user['body']->expires_in;
			$_SESSION['refresh_token'] = $user['body']->refresh_token;
		}
		catch(Exception $e){
			echo "Exception: ",  $e->getMessage(), "\n";
		}
	} 
	else {
		if($_SESSION['expires_in'] < time()) {
			try {
				$refresh = $meli->refreshAccessToken();
				$_SESSION['access_token'] = $refresh['body']->access_token;
				$_SESSION['expires_in'] = time() + $refresh['body']->expires_in;
				$_SESSION['refresh_token'] = $refresh['body']->refresh_token;
			} 
			catch (Exception $e) {
			  	echo "Exception: ",  $e->getMessage(), "\n";
			}
		}
	}
	echo '<pre>';
		print_r($_SESSION);
	echo '</pre>';
try {
	$token = $_SESSION['access_token'];
	$stmt = $conn->prepare("UPDATE info SET token = '$token' WHERE id = 1");
	$stmt->execute(array(
	  ':id'   => 1,
	  ':token' => $token
	));   
	echo $stmt->rowCount(); 
  } 
  catch(PDOException $e) {
    echo $sql . "<br>" . $e->getMessage();
  }

$conn = null;
} 
else {
	echo '<a href="' . $meli->getAuthUrl($redirectURI, Meli::$AUTH_URL[$siteId]) . '">Login using MercadoLibre oAuth 2.0</a>';
}

